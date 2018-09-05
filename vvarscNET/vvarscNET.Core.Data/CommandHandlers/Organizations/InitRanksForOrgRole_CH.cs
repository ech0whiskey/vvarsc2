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
    /// <summary>
    /// Special Version of Handler that Uses Strings to look-up
    /// PayGrades instead of int (as would be passed-in from client)
    /// </summary>
    public class InitRanksForOrgRole_CH : ICommandHandler<InitRanksForOrgRole_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public InitRanksForOrgRole_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, InitRanksForOrgRole_C command)
        {
            if (command.SupportedRanks == null || command.SupportedRanks.Count < 1)
                throw new ArgumentNullException(nameof(command.SupportedRanks));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd0 = @"
                    DELETE m
                    from Organizations.RankOrgRoleMap m
                    where m.OrgRoleID = @OrgRoleID;
                ";

                var cmd = @"
                    INSERT INTO [Organizations].[RankOrgRoleMap] (
	                    RankID
	                    ,OrgRoleID
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    )
                    select
	                    r.ID
	                    ,@OrgRoleID
	                    ,1 [IsActive]
	                    ,@CreatedOn
	                    ,@CreatedBy
	                    ,@ModifiedOn
	                    ,@ModifiedBy
                    from [Organizations].[Ranks] r
                    where r.ID in @SupportedRanks
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
                result.StatusDescription = "OrgRole Ranks Added Successfully!";
                return result;
            }
        }
    }
}
