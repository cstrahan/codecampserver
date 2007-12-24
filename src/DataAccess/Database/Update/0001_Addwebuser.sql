IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'webuser')
DROP LOGIN [webuser]

CREATE LOGIN [webuser] WITH PASSWORD=N'password', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO