using vvarscNET.Core.CommandModels.People;
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

namespace vvarscNET.Core.Data.CommandHandlers.People
{
    public class AddOrgRolesToPayGrade_CH : ICommandHandler<AddOrgRolesToPayGrade_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public AddOrgRolesToPayGrade_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, AddOrgRolesToPayGrade_C command)
        {
            if (command.SupportedOrgRoles == null || command.SupportedOrgRoles.Count < 1)
                throw new ArgumentNullException(nameof(command.SupportedOrgRoles));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    INSERT INTO [People].[PayGradeOrgRoleMap] (
	                    PayGradeID
	                    ,OrgRoleID
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    )
                    select
	                    @PayGradeID
	                    ,r.ID
	                    ,1 [IsActive]
	                    ,@CreatedOn
	                    ,@CreatedBy
	                    ,@ModifiedOn
	                    ,@ModifiedBy
                    from [People].OrgRoles r
                    where r.RoleName in @SupportedOrgRoles
                        and r.IsActive = 1
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            PayGradeID = command.PayGradeID,
                            SupportedOrgRoles = command.SupportedOrgRoles,
                            IsActive = command.IsActive,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);
                        
                        if (rowsAffected == command.SupportedOrgRoles.Count)
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
                result.StatusDescription = "PayGrade OrgRoles Added Successfully!";
                return result;
            }
        }
    }
}
