@echo off
cd /d %~dp0
echo : SETUP BAT
echo :
echo : Let StateGo File(.psgg) to be associated with StrateGo app
echo : StateGo�t�@�C��(.psgg)��StateGo�A�v���Ɋ֘A�t�����܂��B
echo : 
echo : NOTE THAT EXECUTE THIS BATCH AS ADMINISTRATOR.
echo : �Ǘ��҃��[�h�Ŏ��s���Ă��������B
echo :
pause
assoc .psgg=PSGGFILE
ftype PSGGFILE="%~dp0StateGo.exe" "%%L"
pause
