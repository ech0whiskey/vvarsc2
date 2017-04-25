using vvarscNET.Core.CommandModels.Members;
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

namespace vvarscNET.Core.Data.CommandHandlers.Members
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
            if (string.IsNullOrEmpty(command.MemberPID))
                throw new ArgumentNullException(nameof(command.MemberPID));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update [Users].[Members] set
                        FirstName = @FirstName,
                        LastName = @LastName
                    where MemberPID = @MemberPID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new {
                            FirstName = command.FirstName
                            ,LastName = command.LastName
                            ,MemberPID = command.MemberPID
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

                result.ItemIDs.Add(new CompositeID {IDType = "MemberPID", IDValue = command.MemberPID });
                result.Status = HttpStatusCode.OK;
                result.StatusDescription = "Member updated Successfully!";
                return result;
            }
        }
    }
}
