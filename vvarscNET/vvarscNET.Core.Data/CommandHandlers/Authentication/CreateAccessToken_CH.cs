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
using vvarscNET.Model.ResponseModels.Authentication;

namespace vvarscNET.Core.Data.CommandHandlers.Authentication
{
    public class CreateAccessToken_CH : ICommandHandler<CreateAccessToken_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public CreateAccessToken_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, CreateAccessToken_C command)
        {
            if (string.IsNullOrEmpty(command.MemberPID))
                throw new ArgumentNullException(nameof(command.MemberPID));

            if (string.IsNullOrEmpty(command.ApplicationPID))
                throw new ArgumentNullException(nameof(command.ApplicationPID));

            if (string.IsNullOrEmpty(command.TokenType))
                throw new ArgumentNullException(nameof(command.TokenType));

            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                CreateAccessToken_CRM cmdResult = null;

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        cmdResult = connection.Query<CreateAccessToken_CRM>("[ATCS].[CreateAccessToken2]", new {
                            MemberPID = command.MemberPID,
                            AppID = command.AppID,
                            ExpiryDate = command.ExpiryDate,
                            OfflineExpiryDate = command.OfflineExpiryDate,
                            ShellID = command.ShellID,
                            ApplicationPID = command.ApplicationPID,
                            TokenType = command.TokenType,
                            ParentToken = command.ParentToken,
                            Version = command.Version
                        }, transaction, commandType: System.Data.CommandType.StoredProcedure).First();

                        if (cmdResult.AccessToken != null)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            result.Status = HttpStatusCode.InternalServerError;
                            result.StatusDescription = "Inserted row count does not match submitted row count. Transaction rolled back.";
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
                    IDValue = cmdResult.AccessToken.ToString()
                });
                result.ItemIDs.Add(new CompositeID {
                    IDType = "PrivateKey",
                    IDValue = cmdResult.PrivateKey
                });

                result.Status = HttpStatusCode.OK;
                result.StatusDescription = "AccessToken Created Successfully!";
                return result;
            }
        }
    }
}
