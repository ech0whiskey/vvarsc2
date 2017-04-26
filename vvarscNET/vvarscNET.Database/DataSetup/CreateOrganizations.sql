DECLARE @OrgsToCreate TABLE (
	RowID int identity(1,1)
	,OrganizationName [dbo].Name
	,OrganizationSpectrumID nvarchar(10)
	,OrganizationWebsiteURL [dbo].Name
	,Existing bit
)

insert into @OrgsToCreate (OrganizationName, OrganizationSpectrumID, OrganizationWebsiteURL)
values
('VVarMachine','WARMACHINE','http://www.vvarmachine.com')


DECLARE @ExistingOrganizations TABLE (
	OrganizationSpectrumID nvarchar(10)
)

--Get Existing Orgs in DB
insert into @ExistingOrganizations (OrganizationSpectrumID)
select distinct
	o.OrganizationSpectrumID
from Organizations.Organizations o

--Update our Staging Table so only new Users are Added
update c set
	c.Existing =
	case
		when e.OrganizationSpectrumID is null then 0
		else 1
	end
from @OrgsToCreate c
left join @ExistingOrganizations e
	on e.OrganizationSpectrumID = c.OrganizationSpectrumID

--Main Loop For Creating Members and their Credentials
DECLARE @Cnt int = 1
DECLARE @Total int = (select COUNT(1) from @OrgsToCreate)

DECLARE @OrganizationName [dbo].Name
	,@OrganizationSpectrumID nvarchar(10)
	,@OrganizationWebsiteURL [dbo].Name
	,@Existing bit

WHILE (@Cnt <= @Total)
BEGIN
	select
		@OrganizationName = c.OrganizationName
		,@OrganizationSpectrumID = c.OrganizationSpectrumID
		,@OrganizationWebsiteURL = c.OrganizationWebsiteURL
		,@Existing = c.Existing
	from @OrgsToCreate c
	where c.RowID = @Cnt

	if (@Existing = 0)
	BEGIN
		insert into [Organizations].[Organizations] (OrganizationName, OrganizationSpectrumID, OrganizationWebsiteURL, IsActive, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
		select
			@OrganizationName
			,@OrganizationSpectrumID
			,@OrganizationWebsiteURL
			,1 [IsActive]
			,SYSUTCDATETIME() [CreatedOn]
			,'DataSetup' [CreatedBy]
			,SYSUTCDATETIME() [ModifiedOn]
			,'DataSetup' [ModifiedBy]

		PRINT '--Inserted Organization: ' + @OrganizationName
	END

	set @Cnt += 1
END