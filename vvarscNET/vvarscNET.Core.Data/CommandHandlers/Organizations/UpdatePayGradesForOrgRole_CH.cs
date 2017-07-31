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
    public class UpdatePayGradesForOrgRole_CH : ICommandHandler<UpdatePayGradesForOrgRole_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public UpdatePayGradesForOrgRole_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, UpdatePayGradesForOrgRole_C command)
        {
            if (command.SupportedPayGrades == null || command.SupportedPayGrades.Count < 1)
                throw new ArgumentNullException(nameof(command.SupportedPayGrades));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd0 = @"
                    DELETE m
                    from Organizations.PayGradeOrgRoleMap m
                    where m.OrgRoleID = @OrgRoleID;
                ";

                var cmd = @"
                    INSERT INTO [Organizations].[PayGradeOrgRoleMap] (
	                    PayGradeID
	                    ,OrgRoleID
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    )
                    select
	                    pg.ID
	                    ,@OrgRoleID
	                    ,1 [IsActive]
	                    ,@CreatedOn
	                    ,@CreatedBy
	                    ,@ModifiedOn
	                    ,@ModifiedBy
                    from [People].[PayGrades] pg
                    where pg.ID in @SupportedPayGrades
                        and pg.IsActive = 1
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected0 = connection.Execute(cmd0, new
                        {
                            OrgRoleID = command.OrgRoleID
                        }, transaction);

                        int rowsAffected = connection.Execute(cmd, new
                        {
                            OrgRoleID = command.OrgRoleID,
                            SupportedPayGrades = command.SupportedPayGrades,
                            IsActive = true,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);
                        
                        if (rowsAffected == command.SupportedPayGrades.Count)
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
