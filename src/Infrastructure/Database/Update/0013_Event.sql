/*
Run this script on:

        localhost\sqlexpress.CodeCampServerVersioned    -  This database will be modified

to synchronize it with:

        localhost\sqlexpress.CodeCampServer

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 9/21/2009 4:53:45 PM

*/
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
BEGIN TRANSACTION
GO
select * into conference_migration from conferences
GO
PRINT N'Altering [dbo].[Conferences]'
GO
ALTER TABLE [dbo].[Conferences] DROP
COLUMN [ConferenceKey],
COLUMN [Name],
COLUMN [Description],
COLUMN [StartDate],
COLUMN [EndDate],
COLUMN [LocationName],
COLUMN [Address],
COLUMN [City],
COLUMN [Region],
COLUMN [PostalCode],
COLUMN [TimeZone],
COLUMN [LocationUrl]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Events]'
GO
CREATE TABLE [dbo].[Events]
(
[Id] [uniqueidentifier] NOT NULL,
[EventKey] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[LocationName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LocationUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Region] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TimeZone] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserGroupID] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Meetings]'
GO
CREATE TABLE [dbo].[Meetings]
(
[Id] [uniqueidentifier] NOT NULL,
[Topic] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Summary] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SpeakerName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SpeakerBio] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SpeakerUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
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
