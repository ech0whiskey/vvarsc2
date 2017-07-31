CREATE TABLE [Organizations].[Units] (
	ID int identity NOT NULL
	,OrganizationID int NOT NULL
	,UnitName [dbo].Name
	,UnitFullName [dbo].Name
	,UnitDesignation [dbo].Name
	,UnitDescription nvarchar(max)
	,UnitType [dbo].Enum
	,DivisionID int NOT NULL
	,IsHidden bit
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Organizations_Units] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
	,CONSTRAINT [FK_Divisions_Units] FOREIGN KEY (DivisionID) REFERENCES [Organizations].[Divisions](ID)
)