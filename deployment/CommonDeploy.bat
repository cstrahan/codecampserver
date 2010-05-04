SET projectName=%2
SET databaseServer=%1
SET reload=%3

SET databaseName=%projectName%
SET websiteTargetDir=C:\inetpub\%projectName%
SET connectionString=Data Source=%databaseServer%;Initial Catalog=%databaseName%;Integrated Security=SSPI;
SET databaseIntegrated=true
SET agentsTargetDir=C:\inetpub\%projectName%\BatchAgents

nant\nant.exe -buildfile:deployment.build -D:connection.string="%connectionString%" -D:website.target.dir="%websiteTargetDir%" -D:database.server="%databaseServer%" -D:database.name="%databaseName%" -D:database.integrated="%databaseIntegrated%" -D:reloaddatabase=%reload%  -D:agents.target.dir="%agentsTargetDir%" deploy