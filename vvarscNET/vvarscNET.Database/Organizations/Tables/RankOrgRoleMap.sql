CREATE TABLE [Organizations].[RankOrgRoleMap] (
	RankID int NOT NULL
	,OrgRoleID int NOT NULL
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_RankOrgRoleMap] PRIMARY KEY CLUSTERED (RankID, OrgRoleID)
	,CONSTRAINT [FK_Ranks_PayGradeOrgRoleMap] FOREIGN KEY (RankID) REFERENCES [Organizations].[Ranks] (ID)
	,CONSTRAINT [FK_OrgRoles_PayGradeRoleMap] FOREIGN KEY (OrgRoleID) REFERENCES [Organizations].[OrgRoles] (ID)
)
