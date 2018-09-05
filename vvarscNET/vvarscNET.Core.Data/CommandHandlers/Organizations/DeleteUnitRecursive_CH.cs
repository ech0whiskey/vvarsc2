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
    public class DeleteUnitRecursive_CH : ICommandHandler<DeleteUnitRecursive_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public DeleteUnitRecursive_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, DeleteUnitRecursive_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    ;with units as (
	                    select
		                    u.*
	                    from [Organizations].Units u
	                    where u.ID = @UnitID
	                    union all
	                    select
		                    u.*
	                    from [Organizations].Units u
	                    join units tu
		                    on tu.ID = u.ParentUnitID
                    )

                    delete u
                    from Organizations.Units u
                    join units tu
	                    on tu.ID = u.ID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            UnitID = command.UnitID
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
                result.StatusDescription = "Unit Deleted Successfully!";
                return result;
            }
        }
    }
}
