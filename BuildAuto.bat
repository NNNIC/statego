@echo off
cd /d %~dp0
echo : Start Auto Build

if "%MSBUILD17%"=="" set MSBUILD17=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe
if "%NUGET%"=="" set NUGET=C:\nuget\nuget.exe
set CFG=Release

if not exist "%MSBUILD17%" (
    echo MSBuild not found at %MSBUILD17%
    exit /b 1
)
if not exist "%NUGET%" (
    echo Nuget not found at %NUGET%
    echo Warning: Continuing without Nuget check...
)

md Work 2>nul

echo : ------------------------------
echo : build Version Maker
echo : ------------------------------
pushd Tools\version-maker\vm
    "%MSBUILD17%" vm.sln /t:Build /p:Configuration=Release
popd

echo : ------------------------------
echo : build VisualStudioFileOpenTool
echo : ------------------------------
md Work\VisualStudioFileOpenTool 2>nul
if not exist Work\VisualStudioFileOpenTool\.git git clone https://github.com/zhsfei/VisualStudioFileOpenTool2022 Work\VisualStudioFileOpenTool
pushd Work\VisualStudioFileOpenTool
    "%MSBUILD17%" AtomicTorch.VisualStudioFileOpenTool.sln /t:Build /p:Configuration=%CFG%
popd

echo : ------------
echo : build editor
echo : ------------
pushd m1\StateViewer
    "%NUGET%" restore StateViewer.sln
    "%MSBUILD17%" StateViewer.sln /t:StateViewer:Rebuild /p:Configuration=%CFG%
popd

echo : ---------------
echo : get starter-kit
echo : ---------------
pushd Work
    md starterkit 2>nul
    if not exist starterkit\.git git clone https://github.com/NNNIC/psgg-starter-kit.git starterkit
    pushd starterkit
        git rev-parse HEAD > ..\head.txt
    popd
    for /f %%i in (head.txt) do set rev=%%i
    echo https://github.com/NNNIC/psgg-starter-kit.git %rev% > starterkit\starterkit\starter-kit.txt
popd

echo : -----------
echo : get mermaid
echo : -----------
pushd Work
    md mermaid 2>nul
    pushd mermaid
        if not exist psgg-mermaid-flow.zip (
             curl -L -O https://github.com/NNNIC/psgg-mermaid-flow/releases/download/1.2/psgg-mermaid-flow.zip
             powershell expand-archive psgg-mermaid-flow.zip ./ -Force
        )
    popd
popd

echo : --------------
echo : build conveter
echo : --------------
echo : Integrated into StateViewer.sln - Skipping separate clone/build
:: pushd Work
::     md conv 2>nul
::     if not exist conv\.git git clone https://github.com/NNNIC/psgg-converter.git conv
::     pushd conv
::         git rev-parse HEAD > ..\head.txt
::     popd
::     set hash=
::     for /f %%i in (head.txt) do if "%hash%"=="" set hash=%%i
::     md conv\psggConverter\psggConverterLib 2>nul
::     echo namespace psggConverterLib { public class githash { public const string hash="%hash%"; }} > conv\psggConverter\psggConverterLib\githash.cs
::     pushd conv\psggConverter
::         "%MSBUILD17%" psggConverter.sln /t:psggConverter:Rebuild /p:Configuration=%CFG%
::     popd
:: popd

echo : COPY
set SRC=%CD%
set TGT=%SRC%\m1\StateViewer\StateViewer\bin\%CFG%
robocopy %SRC%\m1\StateViewer\ini %TGT%\ini /MIR
robocopy %SRC%\Others\state-images %TGT%\state-images /MIR
robocopy %SRC%\Work\mermaid\psgg-mermaid-flow\psgg2mermaid\bin\%CFG% %TGT%
robocopy %SRC%\Work\starterkit\starterkit2  %TGT%\starterkit2 /S /E
md %TGT%\tools 2>nul
robocopy %SRC%\Work\VisualStudioFileOpenTool  %TGT%\tools LICENSE.md
robocopy %SRC%\Work\VisualStudioFileOpenTool\VisualStudioFileOpenTool\bin\%CFG% %TGT%\tools *.*
:: robocopy %SRC%\Work\conv\psggConverter\psggConverterLib\bin\%CFG% %TGT% *.*
copy %SRC%\Others\archivebatch\__setup.bat %TGT%\*.*

cd /d %~dp0
echo : COPY Winpython
:: robocopy %SRC%\winPython %TGT%\winPython /MIR

echo : BUILD FINISHED
