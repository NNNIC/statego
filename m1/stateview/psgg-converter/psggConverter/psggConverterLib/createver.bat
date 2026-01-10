cd /d %~dp0

git remote -v > ~url.txt
set RURL=
for /f "tokens=2*" %%i in (~url.txt) do if "%RURL%"=="" set RURL=%%i
echo RURL=%RURL%
::pause

echo namespace psggConverterLib { public class ver { public const string version="0.31.0"; public const string datetime="%DATE%-%TIME%";  public const string depot="%RURL%";  } } > ver.cs