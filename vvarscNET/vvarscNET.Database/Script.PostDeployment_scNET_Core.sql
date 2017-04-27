﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
PRINT 'Starting Post-Deployment'

PRINT ''
print 'Executing Script: CreateOrganizations.sql'
:r .\DataSetup\CreateOrganizations.sql
GO

PRINT ''
print 'Executing Script: CreateUsers.sql'
:r .\DataSetup\CreateUsers.sql
GO

PRINT ''