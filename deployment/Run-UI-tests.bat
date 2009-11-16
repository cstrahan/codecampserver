set port=85
call CommonDeploy.bat .\sqlexpress codecampserver_local true
start /min tests\tools\cassini\ConsoleCassini-v35.exe  c:\inetpub\codecampserver_local %port%  /
nant\nant.exe -buildfile:deployment.build pokeUIConfig -D:uitesturl=http://localhost:%port%
tests\tools\nunit\nunit-console.exe  tests\codecampserver.uitests.dll /noshadow /nologo /xml=uitests.results.xml
taskkill /IM consolecassini-v35.exe
