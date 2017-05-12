using vvarscNET.Core.CommandModels.Organizations;
using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Result;
using vvarscNET.Core.Factories;
using System.Net;
using Dapper;

namespace vvarscNET.Core.Data.CommandHandlers.Organizations
{
    public class CreateOrgRole_CH : ICommandHandler<CreateOrgRole_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public CreateOrgRole_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, CreateOrgRole_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    INSERT INTO [Organizations].[Roles] (
	                    RoleName
	                    ,RoleShortName
	                    ,RoleDisplayName
                        ,RoleType
                        ,RoleOrderBy
	                    ,IsActive
	                    ,IsHidden
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    ) VALUES (
	                    @RoleName
	                    ,@RoleShortName
	                    ,@RoleDisplayName
                        ,@RoleType
                        ,@RoleOrderBy
	                    ,@IsActive
	                    ,@IsHidden
	                    ,@CreatedOn
	                    ,@CreatedBy
	                    ,@ModifiedOn
	                    ,@ModifiedBy
                    )

                    SELECT CAST(SCOPE_IDENTITY() as int)
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int? id = connection.Query<int>(cmd, new
                        {
                            RoleName = command.RoleName,
                            RoleShortName = command.RoleShortName,
                            RoleDisplayName = command.RoleDisplayName,
                            RoleType = command.RoleType,
                            RoleOrderBy = command.RoleOrderBy,
                            IsActive = command.IsActive,
                            IsHidden = command.IsHidden,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction).FirstOrDefault();

                        if (id != null)
                        {
                            transaction.Commit();
                            result.ItemIDs.Add(id.ToString());
                        }
                        else
                        {
                            transaction.Rollback();
                            result.Status = HttpStatusCode.InternalServerError;
                            result.StatusDescription = "Updated row count does not match submitted row count. Transaction rolled back.";
                            return result;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                result.Status = HttpStatusCode.OK;
                result.StatusDescription = "OrgRole Created Successfully!";
                return result;
            }
        }
    }
}
