CREATE TABLE [Organizations].[RankUnitOrgRoleMap] (
	RankID int NOT NULL
	,OrgRoleID int NOT NULL
	,UnitID int NOT NULL
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_RankUnitOrgRoleMap] PRIMARY KEY CLUSTERED (RankID, OrgRoleID, UnitID)
	,CONSTRAINT [FK_PayGrades_RankUnitOrgRoleMap] FOREIGN KEY (RankID) REFERENCES [Organizations].[Ranks] (ID)
	,CONSTRAINT [FK_OrgRoles_RankUnitOrgRoleMap] FOREIGN KEY (OrgRoleID) REFERENCES [Organizations].[OrgRoles] (ID)
)
