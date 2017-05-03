CREATE TABLE [Authentication].[Credentials] (
	ID int identity(1,1)
	,MemberID int
	,UserName [dbo].Name NOT NULL UNIQUE
	,PasswordHash [dbo].Name
	,OrganizationID int NULL
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Credentials] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Members_Credentials] FOREIGN KEY (MemberID) REFERENCES [People].[Members](ID)
	,CONSTRAINT [FK_Organizations_Credentials] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
)
