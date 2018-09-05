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
    public class UpdateUnit_CH : ICommandHandler<UpdateUnit_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public UpdateUnit_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, UpdateUnit_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update u set
	                    u.ParentUnitID = @ParentUnitID
	                    ,u.UnitName = @UnitName
	                    ,u.UnitFullName = @UnitFullName
	                    ,u.UnitDesignation = @UnitDesignation
	                    ,u.UnitDescription = @UnitDescription
	                    ,u.UnitCallsign = @UnitCallsign
	                    ,u.UnitType = @UnitType
	                    ,u.IsHidden = @IsHidden
	                    ,u.IsActive = @IsActive
	                    ,u.ModifiedOn = @ModifiedOn
	                    ,u.ModifiedBy = @ModifiedBy
                    from Organizations.Units u
                    where u.UnitID = @UnitID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            UnitID = command.UnitID,
                            ParentUnitID = command.ParentUnitID,
                            UnitName = command.UnitName,
                            UnitFullName = command.UnitFullName,
                            UnitDesignation = command.UnitDesignation,
                            UnitDescription = command.UnitDescription,
                            UnitCallsign = command.UnitCallsign,
                            UnitType = command.UnitType,
                            IsHidden = command.IsHidden,
                            IsActive = command.IsActive,
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);

                        if (rowsAffected == 1)
                        {
                            transaction.Commit();
                            result.ItemIDs.Add(command.UnitID.ToString());
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
                result.StatusDescription = "Unit Updated Successfully!";
                return result;
            }
        }
    }
}
