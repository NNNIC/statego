@echo off
setlocal EnableDelayedExpansion
cd /d %~dp0

set "ARTIFACT_ZIP=%~1"

if "!ARTIFACT_ZIP!"=="" (
    echo [ERROR] Please specify the path to the downloaded zip file.
    echo Usage: %0 [Path to StateGo-Release.zip]
    exit /b 1
)

if not exist "!ARTIFACT_ZIP!" (
    echo [ERROR] File not found: !ARTIFACT_ZIP!
    exit /b 1
)

if not exist "winPython" (
    echo [ERROR] 'winPython' folder not found in the current directory.
    echo Please run this script from the repository root where 'winPython' folder exists.
    exit /b 1
)

set WORK_DIR=Work\FullArchiveTemp
set OUTPUT_ZIP=StateGo_Full_with_Python.zip

echo : 
echo : Cleaning up work directory...
if exist "%WORK_DIR%" rmdir /s /q "%WORK_DIR%"
md "%WORK_DIR%"

echo : 
echo : Extracting artifact zip...
powershell -Command "Expand-Archive -Path '%ARTIFACT_ZIP%' -DestinationPath '%WORK_DIR%' -Force"

echo : 
echo : Copying winPython...
robocopy winPython "%WORK_DIR%\winPython" /MIR /NFL /NDL /NJH /NJS
:: robocopy returns exit code 1-3 for success, so resetting errorlevel implies failure check logic needs to be careful.
:: But for standard batch execution, we can ignore the exit code unless it's >= 8.
if %ERRORLEVEL% GEQ 8 (
    echo [ERROR] Robocopy failed.
    exit /b 1
)

echo : 
echo : Creating final zip archive...
if exist "%OUTPUT_ZIP%" del "%OUTPUT_ZIP%"
powershell -Command "Compress-Archive -Path '%WORK_DIR%\*' -DestinationPath '%OUTPUT_ZIP%'"

echo : 
echo : Cleanup...
rmdir /s /q "%WORK_DIR%"

echo : 
echo : DONE!
echo : Created: %~dp0%OUTPUT_ZIP%
echo : 
pause
