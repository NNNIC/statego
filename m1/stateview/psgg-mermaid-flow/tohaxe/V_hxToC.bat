@echo off
cd %~dp0

echo :
echo : Remove Work Foloder
set /p a="Y or [enter] ? "

if "%a%"=="Y" (
    echo :rd /s /q out\c 
    rd /s /q out\c 2>nul 
)

:_skiprd
echo : 
echo : ‘ã‘Ö‚Ì‚½‚ßAhx/RegexUtil, hx/SortUtil ‚ðíœ
echo :

del /f src\hx\RegexUtil.hx 2>nul
del /f src\hx\SortUtil.hx 2>nul

echo :
echo : compile
echo : 
Haxe -p src   -m start.Program  --cpp out\c 
echo : done
echo : 
echo : run
echo :
pause
chcp 65001
out\c\Program.exe > a.txt
type a.txt
cmd /k
pause