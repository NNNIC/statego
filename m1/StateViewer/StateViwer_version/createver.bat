::
:: Version��ݒ�
::�@VS : �o�[�W�����ԍ� �� 1.2
::  SD : �{�o�[�W�����̊J�n��
::  MS : �}�C���X�g�[��    
::  MSTXT : �^�C�g���e�L�X�g�p 
:: vm.exe�ɂ��A�J�n������̕������}�C�i�[�o�[�W�����ƂȂ�B
::

set VS=0.70
set ST=2022/05/31
set MS=r0.7
set MSTXT=r0.7

set VM=%~dp0..\..\..\tools\version-maker\bin\vm.exe
dir %VM%
::pause
cd /d %~dp0

"%VM%" "%VS%."  "%ST%" > ~tmp.txt
for /f %%i in (~tmp.txt) do set VER=%%i
set VER
::pause

echo namespace PSGGEditor { public class ver { public static readonly string version="%VER%";    public static readonly string datetime="%DATE%-%TIME%"; public static readonly string milestone="%MS%"; public static readonly string milestonetxt="%MSTXT%"; } } > ver.cs

::pause