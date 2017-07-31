CREATE TABLE [Organizations].[Divisions] (
	ID int identity NOT NULL
	,OrganizationID int NOT NULL
	,DivisionName [dbo].Name
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Divisions] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Organizations_Divisions] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
)