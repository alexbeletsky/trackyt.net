@echo off

SET DIR=%~d0%~p0%

SET web.deploy.folder="${web.deploy.folder}"

echo copy application content
rmdir /s /q %web.deploy.folder%
xcopy /E /F /H /R ..\_PublishedWebSites\Web %web.deploy.folder%
xcopy ..\build_artifacts\_BuildInfo.xml %web.deploy.folder%

echo remove redudant files
del %web.deploy.folder%*Tests*.html
del %web.deploy.folder%Web.Debug.config  
del %web.deploy.folder%Web.Release.config 