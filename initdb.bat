@echo off

SET DIR=%~d0%~p0%

SET database.name="trackytdb"
SET sql.files.directory="%DIR%\db"
SET server.database=".\SQLEXPRESS"
SET version.file="build_output\_BuildInfo.xml"
SET repository.path="git://github.com/alexanderbeletsky/Trackyourtasks.net"
SET version.xpath="//buildInfo/version"
SET environment=LOCAL

"%DIR%deployment\rh\rh.exe" /d=%database.name% /f=%sql.files.directory% /s=%server.database% /vf=%version.file% /vx=%version.xpath% /r=%repository.path% /env=%environment% /simple

