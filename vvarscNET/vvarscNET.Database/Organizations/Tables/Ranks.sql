CREATE TABLE [Organizations].[Ranks] (
	ID int identity(1,1) NOT NULL
	,PayGradeID INT NOT NULL
	,RankName [dbo].Name
	,RankAbbr [dbo].Name
	,RankType [dbo].Enum
	,RankImage [dbo].Name
	,RankGroupName [dbo].Name
	,RankGroupImage [dbo].Name
	,RatingCodeSuffix nvarchar(10)
	,IsActive bit
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_Ranks] PRIMARY KEY CLUSTERED (ID)
	,CONSTRAINT [FK_PayGrades_Ranks] FOREIGN KEY (PayGradeID) REFERENCES [Organizations].[PayGrades](ID)
)
