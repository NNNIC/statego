@echo off
cd /d %~dp0
robocopy G:\statego\tools\psgg-mermaid-flow\tohaxe\out\php\lib .\psgg2mermaid\lib /MIR
pause