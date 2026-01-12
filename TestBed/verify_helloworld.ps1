
# Hello World E2E Verification Script

$exePath = "C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\m1\StateViewer\StateViewer\bin\Debug\StateGo.exe"
$templatePath = "C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\m1\StateViewer\StateViewer\bin\Debug\starterkit2\c-sharp\TestControl.psgg"
$workDirName = "test1"
$workDir = Join-Path $PSScriptRoot $workDirName
$projectName = "test1Control"
$port = 5000

# 1. Cleanup
Write-Host "Cleaning up workspace..."
if (Test-Path $workDir) { Remove-Item $workDir -Recurse -Force }
New-Item -ItemType Directory -Path $workDir | Out-Null

# 2. Launch StateGo in Headless Mode
Write-Host "Launching StateGo..."
# Kill existing processes to free port
Get-Process StateGo -ErrorAction SilentlyContinue | Stop-Process -Force

$argList = @("/new", "/src", $templatePath, "/dir", $workDir, "/name", $projectName)
$process = Start-Process -FilePath $exePath -ArgumentList $argList -PassThru

# Wait for server start
Write-Host "Waiting for server to start..."
Start-Sleep -Seconds 5
$baseUrl = "http://localhost:$port/api"

# Check connection
try {
    Invoke-RestMethod -Uri "$baseUrl/system/info" -Method Get -ErrorAction Stop | Out-Null
    Write-Host "Connected to StateGo."
} catch {
    Write-Error "Failed to connect to StateGo API. Is it running?"
    Stop-Process -Id $process.Id -Force
    exit 1
}

# 3. Batch Modification
Write-Host "Applying modifications via Batch API..."
$batch = @{
    commands = @(
        @{ command = "system/reset" },
        @{ 
            command = "state/create" 
            create_params = @{ name = "S_SAYHELLO"; type = "state"; x = 300; y = 100; comment = "Hello State" } 
        },
        @{
            command = "state/edit"
            edit_params = @{
                name = "S_SAYHELLO"
                params = @{
                    "init" = "System.Console.WriteLine(`"Hello World`");"
                    "nextstate" = "S_END"
                }
            }
        },
        @{
            command = "state/edit"
            edit_params = @{
                name = "S_START"
                params = @{ "nextstate" = "S_SAYHELLO" }
            }
        },
        @{ command = "system/save_and_convert" }
    )
}

$payload = $batch | ConvertTo-Json -Depth 5
Invoke-RestMethod -Uri "$baseUrl/system/batch" -Method Post -Body $payload -ContentType "application/json" | Out-Null
Write-Host "Batch commands sent. Waiting for conversion..."

# Wait for file generation (simple sleep)
Start-Sleep -Seconds 3

# 4. Create Runner
$runnerCode = @"
using System;
public class Runner {
    public static void Main() {
        var sm = new test1Control();
        sm.Run();
    }
}
"@
Set-Content -Path (Join-Path $workDir "Runner.cs") -Value $runnerCode

# 5. Compile
Write-Host "Compiling..."
$sourceFile = Join-Path $workDir "test1Control.cs"
$runnerFile = Join-Path $workDir "Runner.cs"
$outputExe = Join-Path $workDir "Hello.exe"

if (-not (Test-Path $sourceFile)) {
    Write-Error "Generated source file not found: $sourceFile"
    Stop-Process -Id $process.Id -Force
    exit 1
}

# Find csc.exe (assuming typical location or in path)
# Using dotnet cli if available for C# usually needs project, simpler to just assume csc in path or finding it
# Or use PowerShell Add-Type for quick check, but Executable is requested
# Let's try likely csc locations if not in path
$csc = "csc.exe"
if (-not (Get-Command $csc -ErrorAction SilentlyContinue)) {
    $csc = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
}

try {
    & $csc /out:"$outputExe" "$sourceFile" "$runnerFile" | Out-Null
    if (Test-Path $outputExe) {
        Write-Host "Compilation Success."
    } else {
        Write-Error "Compilation Failed."
        exit 1
    }
} catch {
    Write-Error "Compiler error: $_"
    exit 1
}

# 6. Run
Write-Host "Running Application..."
$output = & "$outputExe"
Write-Host "Output: $output"

if ($output -match "Hello World") {
    Write-Host "VERIFICATION PASSED: 'Hello World' found in output."
} else {
    Write-Error "VERIFICATION FAILED: 'Hello World' NOT found."
}

# Cleanup process
Stop-Process -Id $process.Id -Force
