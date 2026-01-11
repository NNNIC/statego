@echo off
cd /d %~dp0

set TL=psgg2mermaid\bin\Debug\psgg2mermaid.exe

del /q testdata_out\*.* 2>nul

echo : targer : FizzBuzzControl.psgg
echo : output : FizzBuzzControl.txt
echo : output : FizzBuzzControl_w_code.txt
%TL% testdata\FizzBuzzControl.psgg testdata_out\FizzBuzzControl.txt
%TL% testdata\FizzBuzzControl.psgg testdata_out\FizzBuzzControl_w_code.txt -c

echo : targer : MazeControl.psgg
echo : output : MazeControl.txt
%TL% testdata\MazeControl.psgg testdata_out\MazeControl.txt

echo : targer : TestControl.psgg
echo : output : TestControl.txt
%TL% testdata\TestControl.psgg testdata_out\TestControl.txt

pause
