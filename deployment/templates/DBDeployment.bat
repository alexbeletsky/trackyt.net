@echo off

SET database.name="${database.name}"
SET sql.files.directory="${dirs.db}"
SET server.database="${server.database}"
SET repository.path="${repository.path}"
SET version.file="${file.version}"
SET version.xpath="//buildInfo/version"
SET environment="${environment}"

echo backup database
call .\scripts\backupdb.bat %server.database% %database.name%
if %ERRORLEVEL% NEQ 0 goto errors

echo update database
"%DIR%rh\rh.exe" /d=%database.name% /f=%sql.files.directory% /s=%server.database% /vf=%version.file% /vx=%version.xpath% /r=%repository.path% /env=%environment% --ni --simple
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:errors
EXIT /B %ERRORLEVEL%

:finish
