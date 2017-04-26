CREATE TABLE [People].[Members] (
	ID int identity(1,1) NOT NULL
	,UserName [dbo].Name NOT NULL
	,RSIHandle [dbo].Name NOT NULL
	,OrganizationID int NOT NULL
	,UserType [dbo].Enum
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED (ID)
)
