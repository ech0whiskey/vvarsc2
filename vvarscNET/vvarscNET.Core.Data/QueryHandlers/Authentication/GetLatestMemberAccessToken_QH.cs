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
            if (string.IsNullOrEmpty(query.MemberPID))
                throw new ArgumentNullException(nameof(query.MemberPID));

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select top 1
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
	                    and dbl.ShellID = @ShellID
                    join Lkp.LK_TokenType lk1
	                    on lk1.TokenType = c.TokenType
                    where m.MemberPID = @MemberPID
	                    and c.ApplicationPID = @ApplicationPID
                    order by
	                    c.RowID desc
                ";

                var res = connection.Query<GetAccessToken_QRM>(sql, new
                {
                    MemberPID = query.MemberPID,
                    ShellID = query.ShellID,
                    ApplicationPID = Globals.ApplicationPID
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
