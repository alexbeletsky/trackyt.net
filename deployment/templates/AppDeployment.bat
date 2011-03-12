@echo off

SET DIR=%~d0%~p0%

SET web.deploy.folder="${web.deploy.folder}"

echo stopping web site..
call %windir%\system32\inetsrv\appcmd stop site ${web.site.name}
if %ERRORLEVEL% NEQ 0 goto errors

echo copy application content
rmdir /s /q %web.deploy.folder%
xcopy /E /F /H /R ..\_PublishedWebSites\Web %web.deploy.folder%
xcopy ..\build_artifacts\_BuildInfo.xml %web.deploy.folder%
if %ERRORLEVEL% NEQ 0 goto errors

echo remove redudant files
del %web.deploy.folder%*Tests*.htm*
del %web.deploy.folder%Web.Debug.config  
del %web.deploy.folder%Web.Release.config 
del %web.deploy.folder%*packages* 
if %ERRORLEVEL% NEQ 0 goto errors

echo starting web site
%windir%\system32\inetsrv\appcmd start site ${web.site.name}
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

:errors
EXIT /B %ERRORLEVEL%

:finish
