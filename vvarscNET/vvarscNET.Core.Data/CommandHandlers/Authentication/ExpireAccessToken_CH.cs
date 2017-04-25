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
            if (string.IsNullOrEmpty(command.MemberPID))
                throw new ArgumentNullException(nameof(command.MemberPID));

            if (string.IsNullOrEmpty(command.AccessTokenID))
                throw new ArgumentNullException(nameof(command.AccessTokenID));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    update c set
	                    c.ExpiryDate = GETDATE()
	                    ,c.OfflineExpiryDate = GETDATE()
                    from [ATCS].[API_Credentials] c
                    where c.MemberPID = @MemberPID
	                    and c.TokenID = @AccessTokenID
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new {
                            MemberPID = command.MemberPID,
                            ShellID = command.ShellID,
                            AccessTokenID = command.AccessTokenID
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

                result.ItemIDs.Add(new CompositeID {
                    IDType = "AccessTokenID",
                    IDValue = command.AccessTokenID
                });

                result.Status = HttpStatusCode.OK;
                result.StatusDescription = "AccessToken Expired Successfully!";
                return result;
            }
        }
    }
}
