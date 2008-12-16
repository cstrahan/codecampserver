CREATE TABLE [dbo].[Management_Application_Instance]
(
[GUID] [uniqueidentifier] NOT NULL DEFAULT (newid()),
[UniqueHostHeader] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ComputerName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ApplicationDomain] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LoadBalanceIP] [varchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AvailableForLoadBalance] [bit] NOT NULL DEFAULT (0),
[MaintenanceHostHeader] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DownForMaintenance] [bit] NOT NULL DEFAULT (0),
[CacheRefreshQueryString] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Created] [datetime] NOT NULL DEFAULT (getdate()),
[Modified] [datetime] NOT NULL DEFAULT (getdate()),
[Version] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO

-- Constraints and indexes
ALTER TABLE [dbo].[Management_Application_Instance] ADD PRIMARY KEY CLUSTERED  ([GUID])
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Unique_LoadBalanceInstance] ON [dbo].[Management_Application_Instance] ([ComputerName], [ApplicationDomain], [LoadBalanceIP])
GO