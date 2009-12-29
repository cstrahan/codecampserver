/*
Run this script on:

        localhost\sqlexpress.CodeCampServerVersioned    -  This database will be modified

to synchronize it with:

        localhost\sqlexpress.CodeCampServer

You are recommended to back up your database before running this script

Script created by SQL Compare version 8.1.0 from Red Gate Software Ltd at 12/29/2009 2:38:19 PM

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
PRINT N'Altering [dbo].[Events]'
GO
ALTER TABLE [dbo].[Events] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Events] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Events] on [dbo].[Events]'
GO
ALTER TABLE [dbo].[Events] ADD CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Conferences]'
GO
ALTER TABLE [dbo].[Conferences] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Conferences] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Conferences] on [dbo].[Conferences]'
GO
ALTER TABLE [dbo].[Conferences] ADD CONSTRAINT [PK_Conferences] PRIMARY KEY CLUSTERED  ([EventId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[UserGroups]'
GO
ALTER TABLE [dbo].[UserGroups] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[UserGroups] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_UserGroups] on [dbo].[UserGroups]'
GO
ALTER TABLE [dbo].[UserGroups] ADD CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Meetings]'
GO
ALTER TABLE [dbo].[Meetings] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Meetings] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Meetings] on [dbo].[Meetings]'
GO
ALTER TABLE [dbo].[Meetings] ADD CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED  ([EventId])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Sponsors]'
GO
ALTER TABLE [dbo].[Sponsors] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Sponsors] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Sponsors] on [dbo].[Sponsors]'
GO
ALTER TABLE [dbo].[Sponsors] ADD CONSTRAINT [PK_Sponsors] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Altering [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ALTER COLUMN [CreatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
ALTER TABLE [dbo].[Users] ALTER COLUMN [UpdatedBy] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Users] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Conferences]'
GO
ALTER TABLE [dbo].[Conferences] ADD
CONSTRAINT [FK_Conferences_EventId_Events_Id] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]),
CONSTRAINT [FK_Conferences_UserGroupId_UserGroups_Id] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroups] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Events]'
GO
ALTER TABLE [dbo].[Events] ADD
CONSTRAINT [FK_Events_UserGroupId_UserGroups_Id] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroups] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Meetings]'
GO
ALTER TABLE [dbo].[Meetings] ADD
CONSTRAINT [FK_Meetings_EventId_Events_Id] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Sponsors]'
GO
ALTER TABLE [dbo].[Sponsors] ADD
CONSTRAINT [FK_Sponsors_UserGroupId_UserGroups_Id] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroups] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[UserGroupAdminUsers]'
GO
ALTER TABLE [dbo].[UserGroupAdminUsers] ADD
CONSTRAINT [FK_UserGroupAdminUsers_UserGroupId_UserGroups_Id] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroups] ([Id]),
CONSTRAINT [FK_UserGroupAdminUsers_UserID_Users_Id] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id])
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
