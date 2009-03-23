rd ..\codeToDeploy\ /s /q
CodeCampServerPackage.exe -o..\CodeToDeploy\ -y
cd ..\CodeToDeploy\
%systemroot%\system32\inetsrv\appcmd stop site codecampserver
dev.bat
%systemroot%\system32\inetsrv\appcmd start site codecampserver