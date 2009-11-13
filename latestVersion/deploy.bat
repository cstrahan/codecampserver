cd %3

SET applicationname=codecampserver

IF "%1"=="" (
SET databaseServer=%1
ELSE(
SET databaseServer=.\sqlexpress)

IF "%2"=="" (
SET instance=%2
ELSE(
SET instance=local)

Set codedir=..\codeToDeploy_%instance%\

SET appinstance=%applicationname%_%instance%
rd %codedir% /s /q
%applicationname%Package.exe -o%codedir% -y
cd %codedir%
cmd /c %systemroot%\system32\inetsrv\appcmd stop site %appinstance%
iisreset
CommonDeploy.bat %databaseserver% %appinstance%
cmd /c %systemroot%\system32\inetsrv\appcmd start site %appinstance%