CREATE TABLE [Organizations].[UnitOrgRoleMap] (
	ID int identity NOT NULL
	,UnitID int NOT NULL
	,OrgRoleID int NOT NULL
	,RatingCodeOverride nvarchar(10)
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_UnitOrgRoles] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Units_UnitOrgRoles] FOREIGN KEY (UnitID) REFERENCES [Organizations].[Units] (ID)
	,CONSTRAINT [FK_OrgRoles_UnitOrgRoles] FOREIGN KEY (OrgRoleID) REFERENCES [Organizations].[OrgRoles] (ID)
)