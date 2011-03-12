@echo off

if '%1' == '' goto usage

sqlcmd -S seekey-note\sqlexpress -i .\scripts\backupdb.sql -v Database = %1 -e
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:usage
echo.
echo Usage: backup.bat [database]
echo [database] - name of database to backup
echo.
EXIT /B 1

:errors
EXIT /B %ERRORLEVEL%

:finish
