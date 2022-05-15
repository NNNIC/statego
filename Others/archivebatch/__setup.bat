@echo off
cd /d %~dp0
echo : SETUP BAT
echo :
echo : Let StateGo File(.psgg) to be associated with StrateGo app
echo : StateGoファイル(.psgg)をStateGoアプリに関連付けします。
echo : 
echo : NOTE THAT EXECUTE THIS BATCH AS ADMINISTRATOR.
echo : 管理者モードで実行してください。
echo :
pause
assoc .psgg=PSGGFILE
ftype PSGGFILE="%~dp0StateGo.exe" "%%L"
pause
