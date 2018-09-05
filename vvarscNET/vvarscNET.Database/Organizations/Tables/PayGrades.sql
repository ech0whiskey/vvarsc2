CREATE TABLE [Organizations].[PayGrades] (
	ID int identity(1,1) NOT NULL
	,PayGradeName [dbo].Name
	,PayGradeDisplayName [dbo].Name
	,PayGradeOrderBy int
	,PayGradeGroup [dbo].Enum
	,PayGradeDescriptionText nvarchar(max)
	,PayGradeNotes nvarchar(max)
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_PayGrades] PRIMARY KEY CLUSTERED (ID)
)
