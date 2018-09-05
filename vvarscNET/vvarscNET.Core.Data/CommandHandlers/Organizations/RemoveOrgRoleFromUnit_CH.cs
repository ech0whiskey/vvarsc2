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
    public class RemoveOrgRoleFromUnit_CH : ICommandHandler<RemoveOrgRoleFromUnit_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public RemoveOrgRoleFromUnit_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, RemoveOrgRoleFromUnit_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    delete m
                    from Organizations.UnitOrgRoleMap m
                    where m.UnitID = @UnitID
	                    and m.OrgRoleID = @OrgRoleID
                ";

                var cmd2 = @"
                    delete m
                    from Organizations.RankUnitOrgRoleMap m
                    where m.UnitID = @UnitID
	                    and m.OrgRoleID = @OrgRoleID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected2 = connection.Execute(cmd2, new
                        {
                            UnitID = command.UnitID,
                            OrgRoleID = command.OrgRoleID
                        }, transaction);

                        int rowsAffected = connection.Execute(cmd, new
                        {
                            UnitID = command.UnitID,
                            OrgRoleID = command.OrgRoleID
                        }, transaction);

                        if (rowsAffected > 0)
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
                result.StatusDescription = "OrgRole Removed From Unit Successfully!";
                return result;
            }
        }
    }
}
