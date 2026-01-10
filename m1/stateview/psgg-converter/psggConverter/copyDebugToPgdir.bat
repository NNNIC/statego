@echo off
cd /d %~dp0
echo :
echo : 本バッチは、Debugのexeを C:\Program files(x86)\PSGGにコピーします。
echo : 
pause

::echo %ProgramFiles(x86)%\PSGG

robocopy psggConverterLib\bin\Debug "%ProgramFiles(x86)%\PSGG"

pause
