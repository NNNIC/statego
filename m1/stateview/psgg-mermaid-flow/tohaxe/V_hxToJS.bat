@echo off
cd %~dp0
del out\js\Test.js 2>nul
echo : 
echo : ��ւ̂��߁Ahx/RegexUtil, hx/SortUtil ���폜
echo :

del /f src\hx\RegexUtil.hx 2>nul
del /f src\hx\SortUtil.hx 2>nul

echo :
echo : compile
echo : 
Haxe -p src\hx_alt  -p src\cs2hx_src -p src\hx -p src\test -p src\sys_cs -m Program  --js out\js\Test.js 
echo : done
echo : 
echo : run
echo :
pause
start out\js\index.html
pause