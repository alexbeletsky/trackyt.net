@echo off

if '%1' == '' goto usage

SET ENV=%1

cd .\code_drop\deployment

echo Deploy database
call .\%ENV%.DbDeployment.bat
if %ERRORLEVEL% NEQ 0 goto errors

echo Deploy application
call .\%ENV%.AppDeployment.bat
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:usage
echo.
echo tracky.net deploy script
echo Usage: deploy.bat [environment]
echo [environment] - deployment environment could be STAGING or PRODUCTION
echo.
EXIT /B 1

:errors
echo Build FAILED
EXIT /B %ERRORLEVEL%

:finish
echo Build SUCCESS
