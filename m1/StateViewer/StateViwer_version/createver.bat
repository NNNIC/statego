::
:: Versionを設定
::　VS : バージョン番号 例 1.2
::  SD : 本バージョンの開始日
::  MS : マイルストーン    
::  MSTXT : タイトルテキスト用 
:: vm.exeにより、開始日からの分数がマイナーバージョンとなる。
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