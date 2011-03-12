@echo off

if '%1' == '' goto usage

SET DIR=%~d0%~p0%

SET database.name="%1"
SET sql.files.directory="%DIR%\db"
SET server.database=".\SQLEXPRESS"
SET version.file="build_output\_BuildInfo.xml"
SET repository.path="git://github.com/alexanderbeletsky/trackyt.net"
SET version.xpath="//buildInfo/version"
SET environment=LOCAL

"%DIR%deployment\rh\rh.exe" /d=%database.name% /f=%sql.files.directory% /s=%server.database% /vf=%version.file% /vx=%version.xpath% /r=%repository.path% /env=%environment% --ni --simple
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:usage
echo.
echo Usage: updatedb.bat [database]
echo [database] - name of database to update
echo.
EXIT /B 1

:errors
EXIT /B %ERRORLEVEL%

:finish
