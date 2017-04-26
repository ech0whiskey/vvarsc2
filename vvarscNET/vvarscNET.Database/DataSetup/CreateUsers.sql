DECLARE @UsersToCreate TABLE (
	RowID int identity(1,1)
	,UserName [dbo].Name
	,RSIHandle [dbo].Name
	,[Password] [dbo].Name
	,OrganizationSpectrumID nvarchar(10)
	,Existing bit
)

insert into @UsersToCreate (UserName, RSIHandle, [Password], OrganizationSpectrumID)
values
('SuperAdmin','SuperAdmin','vvarmachine2017', 'WARMACHINE')


DECLARE @ExistingUsers TABLE (
	UserName [dbo].Name
)

--Get Existing Users in DB
insert into @ExistingUsers (UserName)
select distinct
	m.UserName
from People.Members m

--Update our Staging Table so only new Users are Added
update c set
	c.Existing =
	case
		when e.UserName is null then 0
		else 1
	end
from @UsersToCreate c
left join @ExistingUsers e
	on e.UserName = c.UserName

--Main Loop For Creating Members and their Credentials
DECLARE @Cnt int = 1
DECLARE @Total int = (select COUNT(1) from @UsersToCreate)

DECLARE @UserName [dbo].Name
		,@RSIHandle [dbo].Name
		,@Password [dbo].Name
		,@OrganizationSpectrumID nvarchar(10)
		,@Existing bit
		,@UserID int

WHILE (@Cnt <= @Total)
BEGIN
	select
		@UserName = c.UserName
		,@RSIHandle = c.RSIHandle
		,@OrganizationSpectrumID = c.OrganizationSpectrumID
		,@Password = c.[Password]
		,@Existing = c.Existing
	from @UsersToCreate c
	where c.RowID = @Cnt

	if (@Existing = 0)
	BEGIN
		insert into People.Members (UserName, RSIHandle, OrganizationID, IsActive, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
		select
			@UserName
			,@RSIHandle
			,(select o.ID from [Organizations].[Organizations] o where o.OrganizationSpectrumID = @OrganizationSpectrumID)
			,1 [IsActive]
			,SYSUTCDATETIME() [CreatedOn]
			,'DataSetup' [CreatedBy]
			,SYSUTCDATETIME() [ModifiedOn]
			,'DataSetup' [ModifiedBy]

		set @UserID = SCOPE_IDENTITY()

		PRINT '--Inserted User: ' + @UserName + ', new UserID = ' + cast(@UserID as varchar)

		insert into [Authentication].[Credentials] (MemberID, UserName, PasswordHash, OrganizationID, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
		select
			@UserID
			,@UserName
			,@Password
			,(select o.ID from [Organizations].[Organizations] o where o.OrganizationSpectrumID = @OrganizationSpectrumID)
			,SYSUTCDATETIME() [CreatedOn]
			,'DataSetup' [CreatedBy]
			,SYSUTCDATETIME() [ModifiedOn]
			,'DataSetup' [ModifiedBy]

		PRINT '--Inserted Credential for UserID: ' + cast(@UserID as varchar)
	END

	set @Cnt += 1
END