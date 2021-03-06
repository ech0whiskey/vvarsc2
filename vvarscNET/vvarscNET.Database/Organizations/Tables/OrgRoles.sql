﻿CREATE TABLE [Organizations].[Roles] (
	ID int identity(1,1) NOT NULL
	,OrganizationID int NOT NULL
	,RoleName [dbo].Name
	,RoleShortName [dbo].Name
	,RoleDisplayName [dbo].Name
	,RoleType [dbo].Enum NULL
	,RoleOrderBy int NOT NULL
	,IsActive bit
	,IsHidden bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_OrgRoles] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_Organizations_OrgRoles] FOREIGN KEY (OrganizationID) REFERENCES [Organizations].[Organizations](ID)
)
