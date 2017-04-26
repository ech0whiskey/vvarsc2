using vvarscNET.Core.CommandModels.Authentication;
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

namespace vvarscNET.Core.Data.CommandHandlers.Authentication
{
    public class ExpireAccessToken_CH : ICommandHandler<ExpireAccessToken_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ExpireAccessToken_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, ExpireAccessToken_C command)
        {
            if (string.IsNullOrEmpty(command.AccessToken))
                throw new ArgumentNullException(nameof(command.AccessToken));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update c set
	                    c.ValidTo = SYSUTCDATETIME()
	                    ,c.OfflineExpiryDate = GETDATE()
                    from [Authentication].[Tokens] t
                    where t.MemberID = @MemberID
	                    and t.AccessToken = @AccessToken
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new {
                            MemberID = command.MemberID,
                            AccessToken = command.AccessToken
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

                result.ItemIDs.Add(command.AccessToken.ToString());

                result.Status = HttpStatusCode.OK;
                result.StatusDescription = "AccessToken Expired Successfully!";
                return result;
            }
        }
    }
}
