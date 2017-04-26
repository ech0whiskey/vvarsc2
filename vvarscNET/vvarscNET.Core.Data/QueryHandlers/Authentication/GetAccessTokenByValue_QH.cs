using Dapper;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.Authentication;
using System;
using System.Linq;

namespace vvarscNET.Core.Data.QueryHandlers.Authentication
{
    public class GetAccessTokenByValue_QH : IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetAccessTokenByValue_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetAccessToken_QRM Handle(GetAccessTokenByValue_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    t.ID
	                    ,t.MemberID
	                    ,t.AccessToken
	                    ,t.ParentAccessToken
	                    ,t.ValidFrom
	                    ,t.ValidTo
	                    ,t.OrganizationID
                    from [Authentication].[Tokens] t
                    where t.AccessToken = @AccessToken
                ";

                var res = connection.Query<GetAccessToken_QRM>(sql, new
                {
                    AccessToken = query.AccessToken
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
