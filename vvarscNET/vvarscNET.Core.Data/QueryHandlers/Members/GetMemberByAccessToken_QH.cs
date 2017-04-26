using Dapper;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.Members;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace AdminConsole.Core.Data.QueryHandlers.Members
{
    public class GetMemberByAccessToken_QH : IPermissionQueryHandler<GetMemberByAccessToken_Q, GetMemberByAccessToken_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetMemberByAccessToken_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetMemberByAccessToken_QRM Handle(GetMemberByAccessToken_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    m.ID
	                    ,m.UserName
	                    ,m.RSIHandle
	                    ,m.OrganizationID
	                    ,m.IsActive
	                    ,m.CreatedOn
	                    ,m.CreatedBy
	                    ,m.ModifiedOn
	                    ,m.ModifiedBy
                    from [People].[Members] m
                    join [Authentication].[Token] t
	                    on t.MemberID = m.MemberID
	                    and t.AccessToken = @AccessToken
                    where m.IsActive = 1              
                ";

                var res = connection.Query<GetMemberByAccessToken_QRM>(sql, new {
                    AccessTokenID = query.AccessToken
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
