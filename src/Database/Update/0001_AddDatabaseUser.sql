/*
Script created by SQL Compare version 5.3.0.44 from Red Gate Software Ltd at 3/11/2008 9:10:49 PM
Run this script on .\SQLEXPRESS to make it the same as .\SQLEXPRESS
Please back up your database before running this script

SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'dbuser')
CREATE LOGIN dbuser WITH PASSWORD = 'P@ssword1'
GO
CREATE USER dbuser FOR LOGIN dbuser WITH DEFAULT_SCHEMA=[dbo]
GRANT CONNECT TO dbuser
GO
GRANT CONNECT TO dbuser
GO
PRINT N'Altering members of role db_datareader'
GO
sp_addrolemember N'db_datareader', N'dbuser'
GO
PRINT N'Altering members of role db_datawriter'
GO
sp_addrolemember N'db_datawriter', N'dbuser'
GO
BEGIN TRANSACTION
GO
GRANT CONNECT TO dbuser
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO
*/