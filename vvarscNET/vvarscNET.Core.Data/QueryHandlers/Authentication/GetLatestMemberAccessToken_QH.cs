using Dapper;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.Authentication;
using System;
using System.Linq;

namespace vvarscNET.Core.Data.QueryHandlers.Authentication
{
    public class GetLatestMemberAccessToken_QH : IPermissionQueryHandler<GetLatestMemberAccessToken_Q, GetAccessToken_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetLatestMemberAccessToken_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetAccessToken_QRM Handle(GetLatestMemberAccessToken_Q query)
        {

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select top 1
	                    t.ID
	                    ,t.MemberID
	                    ,t.AccessToken
	                    ,t.ParentAccessToken
	                    ,t.ValidFrom
	                    ,t.ValidTo
	                    ,t.OrganizationID
                    from [Authentication].[Tokens] t
                    where t.MemberID = @MemberID
                        and t.OrganizationID = @OrganizationID
                    order by
	                    t.ID desc
                ";

                var res = connection.Query<GetAccessToken_QRM>(sql, new
                {
                    MemberID = query.MemberID,
                    OrganizationID = query.OrganizationID
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
