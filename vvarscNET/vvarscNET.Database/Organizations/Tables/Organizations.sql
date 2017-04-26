CREATE TABLE [Organizations].[Organizations] (
	ID INT IDENTITY(1,1) NOT NULL
	,OrganizationName [dbo].Name
	,OrganizationSpectrumID nvarchar(10)
	,OrganizationWebsiteURL [dbo].Name
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED (ID)
)
