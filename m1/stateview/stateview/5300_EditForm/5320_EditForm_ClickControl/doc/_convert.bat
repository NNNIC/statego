cd /d %~dp0
set CVT=%~dp0..\..\..\..\..\..\..\bin\Debug\ExcelStateChartConverter.exe
::"%CVT%" EditForm_ClickControl.xlsx ..\created

ExcelStateChartConverter EditForm_ClickControl.xlsx ..\created

::pause