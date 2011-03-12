@echo off

if '%1' == '' goto usage
if '%2' == '' goto usage

sqlcmd -S %1 -i .\scripts\backupdb.sql -v Database = %2 -e
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:usage
echo.
echo Usage: backup.bat [server] [database]
echo [server] - server eg. mymachine\SQLEXPRESS
echo [database] - name of database to backup
echo.
EXIT /B 1

:errors
EXIT /B %ERRORLEVEL%

:finish
