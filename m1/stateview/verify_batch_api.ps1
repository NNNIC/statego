
# Verification Script for System/Batch API

$port = 5000
$found = $false
# Find port
for ($p = 5000; $p -le 5010; $p++) {
    try {
        $uri = "http://localhost:$p/api/system/info"
        $res = Invoke-RestMethod -Uri $uri -Method Get -ErrorAction Stop
        if ($res.success -eq $true) {
            $port = $p
            Write-Host "Found StateGo Server on port $port"
            $found = $true
            break
        }
    } catch {
        # continue
    }
}

if (-not $found) {
    Write-Error "Could not find running StateGo instance on ports 5000-5010. Please start StateGo."
    exit 1
}

$baseUrl = "http://localhost:$port/api"

# 1. Prepare Batch Request
# We will create two states and group them together.
$batch = @{
    commands = @(
        @{
            command = "state/create"
            create_params = @{
                name = "S_Batch1"
                type = "state"
                x = 100
                y = 100
                comment = "Batch created 1"
            }
        },
        @{
            command = "state/create"
            create_params = @{
                name = "S_Batch2"
                type = "state"
                x = 300
                y = 100
                comment = "Batch created 2"
            }
        },
        @{
            command = "group/create"
            group_params = @{
                group_name = "BatchGroup"
                states = @("S_Batch1", "S_Batch2")
                comment = "Group created via batch"
            }
        }
    )
}

$jsonPayload = $batch | ConvertTo-Json -Depth 5
Write-Host "Sending Batch Request..."
Write-Host $jsonPayload

try {
    $response = Invoke-RestMethod -Uri "$baseUrl/system/batch" -Method Post -Body $jsonPayload -ContentType "application/json"
    
    if ($response.success) {
        Write-Host "Batch request SUCCESS"
        $response.data | ForEach-Object { 
            Write-Host " - $($_.command): $($_.success)"
        }
    } else {
        Write-Error "Batch request FAILED: $($response.error)"
        exit 1
    }
} catch {
    Write-Error "Request failed: $_"
    exit 1
}

# 2. Verify existence
$listRes = Invoke-RestMethod -Uri "$baseUrl/state/list" -Method Get
$states = $listRes.data.states
$s1 = $states | Where-Object { $_.name -eq "S_Batch1" }
$s2 = $states | Where-Object { $_.name -eq "S_Batch2" }

if ($s1 -and $s2) {
    Write-Host "Verification PASSED: States created."
} else {
    Write-Error "Verification FAILED: States not found in list."
}

# 3. Cleanup
# We will delete the created items to leave it clean
$cleanupBatch = @{
    commands = @(
        @{
            command = "state/delete"
            delete_params = @{ name = "S_Batch1" }
        },
        @{
            command = "state/delete"
            delete_params = @{ name = "S_Batch2" }
        }
    )
}
$jsonCleanup = $cleanupBatch | ConvertTo-Json -Depth 5
Invoke-RestMethod -Uri "$baseUrl/system/batch" -Method Post -Body $jsonCleanup -ContentType "application/json" | Out-Null
Write-Host "Cleanup completed."
