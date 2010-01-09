echo on
cd %4
SET applicationname=codecampserver
IF "%1"=="" (
SET databaseServer=.\sqlexpress
) ELSE (
SET databaseServer=%1)

IF "%2"=="" (
SET instance=local
) ELSE (
SET instance=%2)

IF "%3"=="" (
set reload=false
) ELSE (
SET reload=%3)

Set codedir=..\codeToDeploy_%instance%\

SET appinstance=%applicationname%_%instance%
rd %codedir% /s /q
%applicationname%Package.exe -o%codedir% -y
cd %codedir%
cmd /c %systemroot%\system32\inetsrv\appcmd stop site %appinstance%
iisreset
CommonDeploy.bat %databaseserver% %appinstance% %reload%
cmd /c %systemroot%\system32\inetsrv\appcmd start site %appinstance%