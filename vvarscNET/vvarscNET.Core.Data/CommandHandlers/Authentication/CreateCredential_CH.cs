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
    public class CreateCredential_CH : ICommandHandler<CreateCredential_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public CreateCredential_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, CreateCredential_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var newToken = Guid.NewGuid().ToString();

                var cmd = @"
                    INSERT INTO [Authentication].[Credentials] (
	                    MemberID
	                    ,UserName
	                    ,PasswordHash
	                    ,OrganizationID
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    ) VALUES (
	                    @MemberID
	                    ,@UserName
	                    ,@PasswordHash
	                    ,@OrganizationID
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
                        var id = connection.Query<int>(cmd, new
                        {
                            MemberID = command.MemberID,
                            UserName = command.UserName,
                            PasswordHash = command.PasswordHash,
                            OrganizationID = command.OrganizationID,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = context.MemberID.ToString(),
                            ModifiedOn = DateTime.UtcNow,
                            ModifiedBy = context.MemberID.ToString()
                        }, transaction);

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
                result.StatusDescription = "Credential Created Successfully!";
                return result;
            }
        }
    }
}
