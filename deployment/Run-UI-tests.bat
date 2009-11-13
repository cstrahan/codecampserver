set port=85
call CommonDeploy.bat .\sqlexpress codecampserver_local true
start /min test\tools\cassini\ConsoleCassini-v35.exe  c:\inetpub\codecampserver_local %port%  /
pause
taskkill /IM consolecassini-v35.exe
