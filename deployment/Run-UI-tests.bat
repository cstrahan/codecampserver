set port=85
call CommonDeploy.bat .\sqlexpress codecampserver_local true
start /min test\tools\cassini\ConsoleCassini-v35.exe  c:\inetpub\codecampserver_local %port%  /
nant\nant.exe -buildfile:deployment.build pokeUIConfig -D:uitesturl=http://localhost:%port%
test\tools\nunit\nunit-console.exe  tests\uitests.dll /noshadow /nologo /xml=uitests.results.xml
taskkill /IM consolecassini-v35.exe
pause
