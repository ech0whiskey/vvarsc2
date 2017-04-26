CREATE TABLE [Authentication].[Credentials] (
	ID int identity(1,1)
	,MemberID int
	,UserName [dbo].Name
	,PasswordHash [dbo].Name
	,OrganizationID int NOT NULL
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Credentials] PRIMARY KEY CLUSTERED (ID)
)
