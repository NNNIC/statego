@echo off
cd %~dp0

echo :
echo : Remove Work Foloder
set /p a="Y or [enter] ? "

if "%a%"=="Y" (
    echo :rd /s /q out\php
    rd /s /q out\php 2>nul 
)

:_skiprd

echo : 
echo : ‘ã‘Ö‚Ì‚½‚ßAhx/RegexUtil, hx/SortUtil ‚ðíœ
echo :

del /f src\lib\util\RegexUtil.hx 2>nul
del /f src\lib\util\SortUtil.hx 2>nul

echo :
echo : compile
echo : 
Haxe -p src -m start.Program  --php out\php 
echo : done
pause
::out\cs\bin\Program.exe
::pause