SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Attendees(
	Id [uniqueidentifier] NOT NULL primary key,
	[Name] [nvarchar](50) NOT NULL,
	Website [nvarchar](4000) NULL,
	Comment nvarchar(4000) null,
	EventId uniqueidentifier not null
) ON [PRIMARY]
GO

ALTER TABLE dbo.Attendees ADD CONSTRAINT
	FK_Attendees_EventId_Events_Id FOREIGN KEY
	(
	EventId
	) REFERENCES dbo.Events
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

CREATE NONCLUSTERED INDEX [IDX_EventId] ON [dbo].[Attendees] 
(
	[EventId] ASC
)
WITH
(
	PAD_INDEX  = OFF,
	STATISTICS_NORECOMPUTE  = OFF,
	SORT_IN_TEMPDB = OFF,
	IGNORE_DUP_KEY = OFF,
	DROP_EXISTING = OFF,
	ONLINE = OFF,
	ALLOW_ROW_LOCKS  = ON,
	ALLOW_PAGE_LOCKS  = ON
)
ON [PRIMARY]