CREATE TABLE [Organizations].[Units] (
	ID int identity NOT NULL
	,ParentUnitID int
	,UnitName [dbo].Name
	,UnitFullName [dbo].Name
	,UnitDesignation [dbo].Name
	,UnitDescription nvarchar(max)
	,UnitCallsign [dbo].Name
	,UnitType [dbo].Enum
	,IsHidden bit
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED (ID)
)