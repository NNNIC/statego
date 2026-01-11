echo y | call ToHx.bat
echo y | call V_hxToPHP.bat

cd /d %~dp0
robocopy out\php\lib "G:\Program Files\Xampp\htdocs\mermaid\psgg2mermaid\lib" /MIR
pause

