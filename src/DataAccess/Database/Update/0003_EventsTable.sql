SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Events(
	Id [uniqueidentifier] NOT NULL primary key,
	[Key] [nvarchar](25) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	StartDate datetime null,
	EndDate datetime null,
	SponsorInfo ntext null,
	LocationName nvarchar(25) null,
	LocationAddress1 nvarchar(4000) null,
	LocationAddress2 nvarchar(4000) null,
	LocationPhoneNumber nvarchar(12) null
) ON [PRIMARY]
GO
 