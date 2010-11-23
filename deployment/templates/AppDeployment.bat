@echo off

SET DIR=%~d0%~p0%

SET web.deploy.folder="${web.deploy.folder}"

rmdir /s /q %web.deploy.folder%
xcopy /E /F /H /R ..\_PublishedWebSites\Web %web.deploy.folder%
xcopy ..\build_artifacts\_BuildInfo.xml %web.deploy.folder%
