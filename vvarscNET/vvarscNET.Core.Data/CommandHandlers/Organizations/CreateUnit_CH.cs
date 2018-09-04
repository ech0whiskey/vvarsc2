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
    public class CreateUnit_CH : ICommandHandler<CreateUnit_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public CreateUnit_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, CreateUnit_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    insert into [Organizations].Units (
	                    ParentUnitID
	                    ,UnitName
	                    ,UnitFullName
	                    ,UnitDesignation
	                    ,UnitDescription
	                    ,UnitCallsign
	                    ,UnitType
	                    ,IsHidden
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    )
                    VALUES (
	                    (select ID from [Organizations].[Units] u where u.UnitName = @ParentUnitName)
	                    ,@UnitName
	                    ,@UnitFullName
	                    ,@UnitDesignation
	                    ,@UnitDescription
	                    ,@UnitCallsign
	                    ,@UnitType
	                    ,@IsHidden
	                    ,@IsActive
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
                            ParentUnitName = command.ParentUnitName,
                            UnitName = command.UnitName,
                            UnitFullName = command.UnitFullName,
                            UnitDesignation = command.UnitDesignation,
                            UnitDescription = command.UnitDescription,
                            UnitCallsign = command.UnitCallsign,
                            UnitType = command.UnitType,
                            IsHidden = command.IsHidden,
                            IsActive = command.IsActive,
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
                result.StatusDescription = "Unit Created Successfully!";
                return result;
            }
        }
    }
}
