CREATE TABLE [People].[MemberRankHistory] (
	RowID INT IDENTITY(1,1) NOT NULL
	,MemberID INT NOT NULL
	,PreviousRankID INT
	,NewRankID INT
	,CreatedOn [dbo].CreatedOn
	,CreatedBy [dbo].CreatedBy
	,ModifiedOn [dbo].ModifiedOn
	,ModifiedBy [dbo].ModifiedBy
	,CONSTRAINT [PK_MemberRankHistory] PRIMARY KEY CLUSTERED (RowID)
	,CONSTRAINT [FK_Members_MemberRankHistory] FOREIGN KEY (MemberID) REFERENCES [People].[Members](ID)
)
