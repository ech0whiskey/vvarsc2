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
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var newToken = Guid.NewGuid().ToString();

                var cmd = @"
                    INSERT INTO [Authentication].[Tokens] (
                        MemberID
                        ,AccessToken
                        ,ParentAccessToken
                        ,ValidFrom
                        ,ValidTo
                        ,OrganizationID
                        ,CreatedOn
                        ,CreatedBy
                    ) VALUES (
                        @MemberID
                        ,@AccessToken
                        ,@ParentAccessToken
                        ,@ValidFrom
                        ,@ValidTo
                        ,@OrganizationID
                        ,SYSUTCDATETIME()
                        ,@CreatedBy
                    )
                ";

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int rowsAffected = connection.Execute(cmd, new
                        {
                            MemberID = command.MemberID,
                            AccessToken = newToken,
                            ParentAccessToken = command.ParentAccessToken,
                            ValidFrom = command.ValidFrom,
                            ValidTo = command.ValidTo,
                            OrganizationID = command.OrganizationID,
                            CreatedBy = context.MemberID
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
                result.StatusDescription = "AccessToken Created Successfully!";
                result.ItemIDs.Add(newToken);
                return result;
            }
        }
    }
}
