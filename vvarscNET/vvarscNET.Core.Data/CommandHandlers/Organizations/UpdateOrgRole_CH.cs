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
    public class UpdateOrgRole_CH : ICommandHandler<UpdateOrgRole_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public UpdateOrgRole_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, UpdateOrgRole_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update r set
	                    r.OrganizationID = @OrganizationID
	                    ,r.RoleName = @RoleName
	                    ,r.RoleShortName = @RoleShortName
	                    ,r.RoleDisplayName = @RoleDisplayName
	                    ,r.RoleType = @RoleType
	                    ,r.RoleOrderBy = @RoleOrderBy
                        ,r.RatingCode = @RatingCode
	                    ,r.IsActive = @IsActive
	                    ,r.IsHidden = @IsHidden
	                    ,r.ModifiedOn = @ModifiedOn
	                    ,r.ModifiedBy = @ModifiedBy
                    from Organizations.OrgRoles r
                    where r.ID = @RoleID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            RoleID = command.ID,
                            OrganizationID = command.OrganizationID,
                            RoleName = command.RoleName,
                            RoleShortName = command.RoleShortName,
                            RoleDisplayName = command.RoleDisplayName,
                            RoleType = command.RoleType,
                            RoleOrderBy = command.RoleOrderBy,
                            RatingCode = command.RatingCode,
                            IsActive = command.IsActive,
                            IsHidden = command.IsHidden,
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);

                        if (rowsAffected == 1)
                        {
                            transaction.Commit();
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
                result.StatusDescription = "OrgRole Updated Successfully!";
                return result;
            }
        }
    }
}
