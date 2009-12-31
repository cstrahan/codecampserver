/*
Script created by SQL Compare version 7.1.0 from Red Gate Software Ltd at 3/24/2009 11:01:56 AM
Run this script on localhost\sqlexpress.CodeCampServerVersioned to make it the same as localhost\sqlexpress.CodeCampServer
Please back up your database before running this script
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
PRINT N'Creating [dbo].[UserGroups]'
GO
CREATE TABLE [dbo].[UserGroups]
(
[Id] [uniqueidentifier] NOT NULL,
[UserGroupKey] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HomepageHTML] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Region] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Country] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GoogleAnalysticsCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DomainName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Users]'
GO
CREATE TABLE [dbo].[Users]
(
[Id] [uniqueidentifier] NOT NULL,
[Username] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PasswordHash] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PasswordSalt] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserGroupId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Tracks]'
GO
CREATE TABLE [dbo].[Tracks]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Conference] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Proposals]'
GO
CREATE TABLE [dbo].[Proposals]
(
[Id] [uniqueidentifier] NOT NULL,
[Conference] [uniqueidentifier] NULL,
[Submitter] [uniqueidentifier] NULL,
[Track] [uniqueidentifier] NULL,
[Level] [int] NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Abstract] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [int] NULL,
[SubmissionDate] [datetime] NULL,
[CreatedDate] [datetime] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Conferences]'
GO
CREATE TABLE [dbo].[Conferences]
(
[Id] [uniqueidentifier] NOT NULL,
[ConferenceKey] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartDate] [datetime] NULL,
[EndDate] [datetime] NULL,
[LocationName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Region] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[HtmlContent] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserGroupID] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[TimeSlots]'
GO
CREATE TABLE [dbo].[TimeSlots]
(
[Id] [uniqueidentifier] NOT NULL,
[StartTime] [datetime] NULL,
[EndTime] [datetime] NULL,
[Conference] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Sessions]'
GO
CREATE TABLE [dbo].[Sessions]
(
[Id] [uniqueidentifier] NOT NULL,
[SessionKey] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Track] [uniqueidentifier] NULL,
[TimeSlot] [uniqueidentifier] NULL,
[Speaker] [uniqueidentifier] NULL,
[Conference] [uniqueidentifier] NULL,
[Level] [int] NULL,
[RoomNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Title] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Abstract] [nvarchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MaterialsUrl] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Speakers]'
GO
CREATE TABLE [dbo].[Speakers]
(
[Id] [uniqueidentifier] NOT NULL,
[SpeakerKey] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Company] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[JobTitle] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Bio] [nvarchar] (3000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[WebsiteUrl] [nvarchar] (400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Attendees]'
GO
CREATE TABLE [dbo].[Attendees]
(
[Id] [uniqueidentifier] NOT NULL,
[FirstName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailAddress] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Webpage] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Status] [int] NULL,
[ConferenceId] [uniqueidentifier] NULL
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
