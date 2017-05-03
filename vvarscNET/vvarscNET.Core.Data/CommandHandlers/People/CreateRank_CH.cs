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
    public class CreateRank_CH : ICommandHandler<CreateRank_C>
    {
        private readonly SQLConnectionFactory _connFactory;

        public CreateRank_CH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Result Handle(IUserContext context, CreateRank_C command)
        {
            Result result = new Result() { Status = HttpStatusCode.BadRequest };

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var cmd = @"
                    INSERT INTO [People].[Ranks] (
	                    PayGradeID
	                    ,RankName
	                    ,RankAbbr
	                    ,RankType
	                    ,RankImage
	                    ,RankGroupName
	                    ,RankGroupImage
	                    ,IsActive
	                    ,CreatedOn
	                    ,CreatedBy
	                    ,ModifiedOn
	                    ,ModifiedBy
                    ) VALUES (
	                    @PayGradeID
	                    ,@RankName
	                    ,@RankAbbr
	                    ,@RankType
	                    ,@RankImage
	                    ,@RankGroupName
	                    ,@RankGroupImage
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
                            PayGradeID = command.PayGradeID,
                            RankName = command.RankName,
                            RankAbbr = command.RankAbbr,
                            RankType = command.RankType,
                            RankImage = command.RankImage,
                            RankGroupName = command.RankGroupName,
                            RankGroupImage = command.RankGroupImage,
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
                result.StatusDescription = "Rank Created Successfully!";
                return result;
            }
        }
    }
}
