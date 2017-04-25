using Dapper;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.Authentication;
using System;
using System.Linq;

namespace vvarscNET.Core.Data.QueryHandlers.Authentication
{
    public class GetAccessTokenByPID_QH : IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetAccessTokenByPID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetAccessToken_QRM Handle(GetAccessTokenByPID_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    c.TokenID [AccessTokenID]
	                    ,m.MemberPID
	                    ,ino.ShellID
	                    ,c.AppID
	                    ,c.ApplicationPID
	                    ,c.ExpiryDate
	                    ,c.OfflineExpiryDate
	                    ,lk1.[Description] [TokenType]
	                    ,c.ParentToken
	                    ,c.PrivateKey
	                    ,c.[Version]
	                    ,m.[Language]
	                    ,m.[Region]
	                    ,m.TimeZone
                    from ATCS.API_Credentials c
                    join Users.Members m
	                    on m.MemberPID = c.MemberPID
                    join Users.In_Owner ino
	                    on ino.InstanceID = m.InstanceID
                    join Accounts.Shell.DBLookUp dbl
	                    on dbl.ShellID = ino.ShellID
                    join Lkp.LK_TokenType lk1
	                    on lk1.TokenType = c.TokenType
                    where CAST(c.TokenID as varchar(100)) = @AccessTokenID
	                    and c.ApplicationPID = @ApplicationPID
                    order by
	                    c.RowID desc
                ";

                var res = connection.Query<GetAccessToken_QRM>(sql, new
                {
                    AccessTokenID = query.AccessTokenID,
                    ApplicationPID = Globals.ApplicationPID
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
