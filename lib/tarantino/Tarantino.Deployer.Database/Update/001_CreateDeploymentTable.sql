/*
Script created by SQL Compare version 5.3.0.44 from Red Gate Software Ltd at 1/29/2008 7:39:39 AM
Run this script on ..Deployer to make it the same as ..Deployer
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
PRINT N'Creating [dbo].[Deployment]'
GO
CREATE TABLE [dbo].[Deployment]
(
[DeploymentId] [uniqueidentifier] NOT NULL,
[Application] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Environment] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Revision] [int] NULL,
[DeployedOn] [datetime] NULL,
[DeployedBy] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CertifiedOn] [datetime] NULL,
[CertifiedBy] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Deployment] on [dbo].[Deployment]'
GO
ALTER TABLE [dbo].[Deployment] ADD CONSTRAINT [PK_Deployment] PRIMARY KEY CLUSTERED  ([DeploymentId])
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