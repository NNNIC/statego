@echo off
set STACKNUM=0
goto :_main

:::: MACRO START ::::
:psgg-macro-start

; commentline format  {%0} will be replaced to a comment.
commentline=::  {%0}

@branch=@@@
<<<?"{%0}"/^brif$/
if [[brcond:{%N}]]  goto :{%1}
>>>
<<<?"{%0}"/^brifc$/
if [[brcond:{%N}]] (
    goto :{%1}
>>>
<<<?"{%0}"/^brelseif$/
) else if [[brcond:{%N}]]  goto :{%1}
>>>
<<<?"{%0}"/^brelseifc$/
) else if [[brcond:{%N}]] (
    goto :{%1}
>>>
<<<?"{%0}"/^brelse$/
) else goto :{%1}
>>>
<<<?"{%0}"/^br_/
$macro:{%0}({%1})$
>>>
@@@

br_YES=@@@
:: br_YES
 if "%YESNO%"=="YES" GOTO :{%1}
@@@

br_NO=@@@
:: br_NO
if not "%YESNO%"=="YES" GOTO :{%1}
@@@

:psgg-macro-end
:::: MACRO END ::::

:_main
cd /d %~dp0

echo : == START OF BATCH ==

goto :S_START

:: [PSGG OUTPUT START] $/./$
    ::              psggConverterLib.dll converted from psgg-file:BuildControl.psgg

    ::
    :S_0000
    ::
    ::
    ::
        @echo off
        cd /d %~dp0
        goto :S_0002
        goto :S_0000
    ::
    :S_0001
    ::
    ::
    ::
        SET DP=pause
        goto :S_0002
        goto :S_0001
    ::
    :S_0002
    ::
    ::
    ::
        echo :
        echo : StateGo build batch
        echo :
        goto :S_0004
        goto :S_0002
    ::
    :S_0004
    ::
    :: 環境存在確認
    ::
        if not exist "%MSBUILD17%" (
            goto :S_0005
        ) else if not exist "%NUGET%" (
            goto :S_0005
        ) else goto :S_0006
        goto :S_0004
    ::
    :S_0005
    ::
    :: 環境エラー
    ::
        echo : ERROR
        echo : Please install Visual Studio 2022 for c++ and c#
        echo :
        pause
        goto :S_END
        goto :S_0005
    ::
    :S_0006
    ::
    :: メニュー表示
    ::
        cd /d %~dp0
        set CFG=
        set TGT=
        set a=
        echo :
        echo : 0. Clean
        echo : 1. Build Debug
        echo : 2. Build Release
        echo : 3. Create Release ZIP ※First Buld Release
        echo : 4. Update existing files in Program Files (*use admin)
        echo : 5. Copy Debug Files to existing files in Program Files(*use admin)
        set /p a="Select : "
        if "%a%"=="0" (
            goto :S_0007
        ) else if "%a%"=="1" (
            goto :S_0008
        ) else if "%a%"=="2" (
            goto :S_0009
        ) else if "%a%"=="3" (
            goto :S_0015
        ) else if "%a%"=="4" (
            goto :S_0018
        ) else if "%a%"=="5" (
            goto :S_0021
        ) else goto :S_0006
        goto :S_0006
    ::
    :S_0007
    ::
    :: Clean
    ::
        cd /d %~dp0
        rd /s /q m1\StateViewer\packages 2>nul
        rd /s /q m1\StateViewer\StateViewer\bin 2>nul
        rd /s /q m1\StateViewer\StateViewer\obj 2>nul
        rd /s /q m1\StateViewer\StateViewer_starter2\bin 2>nul
        rd /s /q m1\StateViewer\StateViewer_starter2\obj 2>nul
        rd /s /q m1\StateViewer\StateViwer_version\bin 2>nul
        rd /s /q m1\StateViewer\StateViwer_version\obj 2>nul
        rd /s /q m1\StateViewer\WordStorage\bin 2>nul
        rd /s /q m1\StateViewer\WordStorage\obj 2>nul
        rd /s /q m1\StateViewer\StateViewer\bin 2>nul
        rd /s /q m1\StateViewer\StateViewer\obj 2>nul
        rd /s /q Work 2>nul
        md Work 2>nul
        goto :S_BACKTO_000
        goto :S_0007
    ::
    :S_0008
    ::
    :: Build Debug
    ::
        set CFG=Debug
        echo :
        echo : Start "Build Debug"
        echo :
        goto :S_0017
        goto :S_0008
    ::
    :S_0009
    ::
    :: Build Relase
    ::
        set CFG=Release
        echo :
        echo : Start "Build Relase"
        echo :
        goto :S_0017
        goto :S_0009
    ::
    :S_0010
    ::
    :: Build Version Maker
    ::
        echo : ------------------------------
        echo : build Version Maker
        echo : ------------------------------
        pushd Tools\version-maker\vm
            "%MSBUILD17%" vm.sln /t:Build /p:Configuration=Release
        popd
        echo : done!
        %DP%
        goto :S_0023
        goto :S_0010
    ::
    :S_0011
    ::
    :: Build Converter
    ::
        echo : --------------
        echo : build conveter
        echo : --------------
        pushd Work
            md conv 2>nul
            git clone https://github.com/NNNIC/psgg-converter.git conv
            pushd conv
                git rev-parse HEAD > ..\head.txt
            popd
            set hash=
            for /f %%i in (head.txt) do if "%hash%"=="" set hash=%%i
            echo :
            echo :  hash=%hash%
            echo :
            echo namespace psggConverterLib { public class githash { public const string hash="%hash%"; }} > conv\psggConverter\psggConverterLib\githash.cs
            echo : type conv\psggConverter\psggConverterLib\githash.cs
            echo : vvvv
            type conv\psggConverter\psggConverterLib\githash.cs
            echo : ^^^^^^^^
            echo : Start build...
            echo : .
            pushd conv\psggConverter
                "%MSBUILD17%" psggConverter.sln /t:psggConverter:Rebuild /p:Configuration=%CFG%
            popd
        popd
        echo : done!
        goto :S_0016
        goto :S_0011
    ::
    :S_0012
    ::
    :: GET MERMAIND
    ::
        echo : -----------
        echo : get mermaid
        echo : -----------
        pushd Work
            md mermaid 2>nul
            pushd mermaid
                curl https://github.com/NNNIC/psgg-mermaid-flow/releases/download/1.2/psgg-mermaid-flow.zip -O -J -L
                echo : got https://github.com/NNNIC/psgg-mermaid-flow/releases/download/1.2/psgg-mermaid-flow.zip
                powershell expand-archive psgg-mermaid-flow.zip ./ -Force
                echo : upziped
            popd
        popd
        echo : done!
        %DP%
        goto :S_0011
        goto :S_0012
    ::
    :S_0013
    ::
    :: GET STARTER KIT
    ::
        echo : ---------------
        echo : get starter-kit
        echo : ---------------
        pushd Work
            md starterkit 2>nul
            git clone https://github.com/NNNIC/psgg-starter-kit.git starterkit
            pushd starterkit
                git rev-parse HEAD > ..\head.txt
            popd
            for /f %%i in (head.txt) do set rev=%%i
            echo : https://github.com/NNNIC/psgg-starter-kit.git %rev%
            echo https://github.com/NNNIC/psgg-starter-kit.git %rev% > starterkit\starterkit\starter-kit.txt
            echo : done !
        popd
        %DP%
        goto :S_0012
        goto :S_0013
    ::
    :S_0014
    ::
    :: BUILD EDITOR
    ::
        echo : ------------
        echo : build editor
        echo : ------------
        git rev-parse HEAD > .\head.txt
        echo :
        echo : get git hash
        set hash=
        for /f %%i in (head.txt) do if "%hash%"=="" set hash=%%i
        echo hash=%hash%
        echo public class githash { public readonly string hash="%hash%"; } > m1\StateViewer\StateViewer\githash.cs
        echo :
        echo : build relase
        pushd m1\StateViewer
            "%NUGET%" restore StateViewer.sln
            "%MSBUILD17%" StateViewer.sln /t:StateViewer:Rebuild /p:Configuration=%CFG%
        popd
        echo : done!
        %DP%
        goto :S_0013
        goto :S_0014
    ::
    :S_0015
    ::
    :: CREATE ZIP
    ::
        robocopy m1\StateViewer\StateViewer\bin\Release Work\archive\zip\StateGo /MIR
        del Work\archive\stagego.zip 2>nul
        powershell compress-archive -Path Work\archive\zip\StateGo -DestinationPath Work\archive\stagego.zip
        echo : Created Zip at %CD%\Work\archive\stagego.zip.
        goto :S_0006
        goto :S_0015
    ::
    :S_0016
    ::
    :: COPY
    ::
        cd /d %~dp0
        set SRC=%CD%
        if "%TGT%"=="" set TGT=%SRC%\m1\StateViewer\StateViewer\bin\%CFG%
        ::_ini
        robocopy %SRC%\m1\StateViewer\ini %TGT%\ini /MIR
        ::_state_images
        robocopy %SRC%\Others\state-images %TGT%\state-images /MIR
        ::_marmaid
        robocopy %SRC%\Work\mermaid\psgg-mermaid-flow\psgg2mermaid\bin\%CFG% %TGT%
        :::_starterkit
        robocopy %SRC%\Work\starterkit\starterkit2  %TGT%\starterkit2 /S /E
        ::_scintilla
        :: copy by msbuild
        ::_vsopentool
        md %TGT%\tools 2>nul
        robocopy %SRC%\Work\VisualStudioFileOpenTool  %TGT%\tools LICENSE.md
        robocopy %SRC%\Work\VisualStudioFileOpenTool\VisualStudioFileOpenTool\bin\%CFG% %TGT%\tools *.*
        ::_conveter
        robocopy %SRC%\Work\conv\psggConverter\psggConverterLib\bin\%CFG% %TGT% *.*
        ::_setup
        copy %SRC%\Others\archivebatch\__setup.bat %TGT%\*.*
        goto :S_BACKTO_000
        goto :S_0016
    ::
    :S_0017
    ::
    :: MD WORK
    ::
        md Work 2>nul
        goto :S_0010
        goto :S_0017
    ::
    :S_0018
    ::
    :: Confirm existing files.
    ::
        if exist "%ProgramFiles(x86)%\PSGG\StateGo.exe" (
            goto :S_0020
        ) else goto :S_0019
        goto :S_0018
    ::
    :S_0019
    ::
    ::
    ::
        echo : %ProgramFiles(x86)%\PSGG\StateGo.exe not found.
        echo : So, Files can not be updated.
        pause
        goto :S_BACKTO_000
        goto :S_0019
    ::
    :S_0020
    ::
    ::
    ::
        echo : Update...
        echo on
        robocopy .\m1\StateViewer\StateViewer\bin\Release "%ProgramFiles(x86)%\PSGG" *.* /S /E
        @echo off
        popd
        pause
        goto :S_BACKTO_000
        goto :S_0020
    ::
    :S_0021
    ::
    :: Confirm existing files.
    ::
        if exist "%ProgramFiles(x86)%\PSGG\StateGo.exe" (
            goto :S_0022
        ) else goto :S_0019
        goto :S_0021
    ::
    :S_0022
    ::
    ::
    ::
        echo : Update...
        echo on
        robocopy .\m1\StateViewer\StateViewer\bin\Debug "%ProgramFiles(x86)%\PSGG" *.*
        @echo off
        popd
        pause
        goto :S_BACKTO_000
        goto :S_0022
    ::
    :S_0023
    ::
    :: Build Visual Studio open tool
    ::
        echo : ------------------------------
        echo : build VisualStudioFileOpenTool
        echo : ------------------------------
        md Work\VisualStudioFileOpenTool 2>nul
        //git clone https://github.com/aienabled/VisualStudioFileOpenTool Work\VisualStudioFileOpenTool
        git clone https://github.com/zhsfei/VisualStudioFileOpenTool2022 Work\VisualStudioFileOpenTool
        pushd Work\VisualStudioFileOpenTool
            "%MSBUILD17%" AtomicTorch.VisualStudioFileOpenTool.sln /t:Build /p:Configuration=%CFG%
        popd
        echo : done!
        %DP%
        goto :S_0014
        goto :S_0023
    ::
    :S_BACKTO_000
    ::
    ::
    ::
        goto :S_0006
        goto :S_BACKTO_000
    ::
    :S_END
    ::
    ::
    ::
        goto :_end
    ::
    :S_START
    ::
    ::
    ::
        goto :S_0000


:: [PSGG OUTPUT END]
echo : == END OF BATCH ==
:_end
pause
