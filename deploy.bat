@echo off

if '%1' == '' goto usage

echo Build and package
call zip.bat
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:usage
echo.
echo tracky.net build script
echo Usage: fullbuild.bat [database]
echo [database] - application database name
echo.
EXIT /B 1

:errors
echo Build FAILED
EXIT /B %ERRORLEVEL%

:finish
echo Build SUCCESS
