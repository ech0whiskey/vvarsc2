CREATE TABLE [Authentication].[Tokens] (
	ID int identity(1,1)
	,MemberID int
	,AccessToken nvarchar(100) NOT NULL
	,ParentAccessToken nvarchar(100) NOT NULL
	,ValidFrom dateTime2 NOT NULL
	,ValidTo dateTime2 NOT NULL
	,OrganizationID int NULL
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Members_Tokens] FOREIGN KEY (MemberID) REFERENCES [People].[Members](ID)
	,CONSTRAINT [FK_Organizations_Tokens] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
)
