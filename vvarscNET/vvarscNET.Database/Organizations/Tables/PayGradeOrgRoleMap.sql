CREATE TABLE [Organizations].[PayGradeOrgRoleMap] (
	PayGradeID int NOT NULL
	,OrgRoleID int NOT NULL
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_RankOrgRoleMap] PRIMARY KEY CLUSTERED (PayGradeID, OrgRoleID)
	,CONSTRAINT [FK_PayGrades_PayGradeOrgRoleMap] FOREIGN KEY (PayGradeID) REFERENCES [People].[PayGrades] (ID)
	,CONSTRAINT [FK_OrgRoles_PayGradeRoleMap] FOREIGN KEY (OrgRoleID) REFERENCES [Organizations].[Roles] (ID)
)
