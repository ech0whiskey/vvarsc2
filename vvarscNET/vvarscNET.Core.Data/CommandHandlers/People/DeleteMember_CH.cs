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
    public class DeleteMember_CH : ICommandHandler<DeleteMember_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public DeleteMember_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, DeleteMember_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };
            result.ItemIDs.Add(command.ID.ToString());

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    --Delete Credential
                    delete c
                    from [Authentication].[Credentials] c
                    where c.MemberID = @MemberID

                    --Delete Tokens
                    delete t
                    from [Authentication].[Tokens] t
                    where t.MemberID = @MemberID

                    --Delete MemberRankHistory
                    delete h
                    from [People].[MemberRankHistory] h
                    where h.MemberID = @MemberID

                    --Delete Member
                    delete m
                    from [People].[Members] m
                    where m.ID = @MemberID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            MemberID = command.ID
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
                result.StatusDescription = "Member Deleted Successfully!";
                return result;
            }
        }
    }
}
