rd ..\codeToDeploy\ /s /q
CodeCampServerPackage.exe -o..\CodeToDeploy\ -y
cd ..\CodeToDeploy\
cmd /c %systemroot%\system32\inetsrv\appcmd stop site codecampserver
iisreset
call dev.bat
cmd /c %systemroot%\system32\inetsrv\appcmd start site codecampserver