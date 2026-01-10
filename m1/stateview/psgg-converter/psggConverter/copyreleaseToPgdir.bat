@echo off
cd /d %~dp0
echo :
echo : 本バッチは、Releaseのexeを C:\Program files(x86)\PSGGにコピーします。
echo : ※ Releaseでしか出ないバグを探るためです。
pause

::echo %ProgramFiles(x86)%\PSGG

robocopy psggConverterLib\bin\Release "%ProgramFiles(x86)%\PSGG"

pause
