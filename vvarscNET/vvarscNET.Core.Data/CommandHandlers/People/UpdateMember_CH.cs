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
    public class UpdateMember_CH : ICommandHandler<UpdateMember_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public UpdateMember_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, UpdateMember_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update m set
	                    m.UserName = @UserName
	                    ,m.RSIHandle = @RSIHandle
	                    ,m.UserType = @UserType
	                    ,m.RankID = @RankID
	                    ,m.IsActive = @IsActive
	                    ,m.ModifiedOn = @ModifiedOn
	                    ,m.ModifiedBy = @ModifiedBy
                    from People.Members m
                    where m.ID = @MemberID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            MemberID = command.ID,
                            UserName = command.UserName,
                            RSIHandle = command.RSIHandle,
                            UserType = command.UserType,
                            RankID = command.RankID,
                            IsActive = command.IsActive,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
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
                result.StatusDescription = "Member Updated Successfully!";
                return result;
            }
        }
    }
}
