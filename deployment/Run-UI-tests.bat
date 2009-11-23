set appname=codecampserver
set port=85
call CommonDeploy.bat .\sqlexpress %appname%_local true
start /min tests\tools\cassini\ConsoleCassini-v35.exe  c:\inetpub\%appname%_local %port%  /
nant\nant.exe -buildfile:deployment.build pokeUIConfig -D:uitesturl=http://localhost:%port%
psr.exe /start /output testrun.zip /gui 0 /sc 1
tests\tools\nunit\nunit-console.exe  tests\%appname%.uitests.dll /noshadow /nologo /xml=uitests.results.xml
psr.exe /stop
taskkill /IM consolecassini-v35.exe
