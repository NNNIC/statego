::
:: Version��ݒ�
::�@VS : �o�[�W�����ԍ� �� 1.2
::  SD : �{�o�[�W�����̊J�n��
::  MS : �}�C���X�g�[��    
::  MSTXT : �^�C�g���e�L�X�g�p 
:: vm.exe�ɂ��A�J�n������̕������}�C�i�[�o�[�W�����ƂȂ�B
::

set VS=0.74
set ST=2024/03/02
set MS=r0.74
set MSTXT=r0.74

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