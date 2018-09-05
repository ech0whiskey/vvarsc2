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
    public class UpdateRanksForUnitOrgRole_CH : ICommandHandler<UpdateRanksForUnitOrgRole_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public UpdateRanksForUnitOrgRole_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, UpdateRanksForUnitOrgRole_C command)
        {
            if (command.SupportedRanks == null || command.SupportedRanks.Count < 1)
                throw new ArgumentNullException(nameof(command.SupportedRanks));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd0 = @"
                    DELETE m
                    from Organizations.RankUnitOrgRoleMap m
                    where m.OrgRoleID = @OrgRoleID
                        and m.UnitID = @UnitID
                ";

                var cmd = @"
                    INSERT INTO [Organizations].[RankUnitOrgRoleMap] (
	                    RankID
	                    ,OrgRoleID
                        ,UnitID
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    )
                    select
	                    r.ID
	                    ,@OrgRoleID
                        ,@UnitID
	                    ,1 [IsActive]
	                    ,@CreatedOn
	                    ,@CreatedBy
	                    ,@ModifiedOn
	                    ,@ModifiedBy
                    from [Organizations].[Ranks] r
                    where r.IsActive = 1
                        and r.ID in @SupportedRanks
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected0 = connection.Execute(cmd0, new
                        {
                            OrgRoleID = command.OrgRoleID,
                            UnitID = command.UnitID
                        }, transaction);

                        int rowsAffected = connection.Execute(cmd, new
                        {
                            OrgRoleID = command.OrgRoleID,
                            SupportedRanks = command.SupportedRanks,
                            IsActive = true,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);
                        
                        if (rowsAffected == command.SupportedRanks.Count)
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
