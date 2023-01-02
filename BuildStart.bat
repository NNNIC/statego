@echo off
cd /d %~dp0
echo :
echo : Set Environmet and Start BuildControl.bat
echo :

if "%MSBUILD17%"=="" set MSBUILD17=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe
if "%NUGET%"==""     set NUGET=C:\nuget\nuget.exe

if exist "%MSBUILD17%" goto :_check1

:_check1
if exist "%NUGET%"     goto :_ok

echo : BULD STOPPED!!
echo : CHECK ENVIRONMENTS
goto :_end


:_ok
echo : READY TO BUILD
pause

call BuildControl.bat

:_end
pause
goto :eof



