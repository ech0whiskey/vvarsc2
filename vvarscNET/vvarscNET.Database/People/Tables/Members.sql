CREATE TABLE [People].[Members] (
	ID int identity(1,1) NOT NULL
	,UserName [dbo].Name NOT NULL UNIQUE
	,RSIHandle [dbo].Name NOT NULL
	,OrganizationID int NULL
	,UserType [dbo].Enum
	,RankID int NULL
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Organizations_Members] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
	,CONSTRAINT [FK_Ranks_Members] FOREIGN KEY (RankID) REFERENCES [People].[Ranks](ID)
)
