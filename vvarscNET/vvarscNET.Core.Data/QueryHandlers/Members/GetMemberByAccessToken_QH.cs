using Dapper;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.Members;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.Members
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
	                    m.MemberPID
	                    ,m.[Login]
	                    ,m.[Password]
                        ,m.FirstName
                        ,m.LastName
                        ,m.[Language]
                        ,m.Region
                        ,m.TimeZone
                        ,ino.ShellID
                        ,m.[Status]
                        ,m.[MemberType]
                        ,m.Customer_ID
                    from [ATCS].[API_Credentials] c
                    join [Users].[Members] m
	                    on m.MemberPID = c.MemberPID
	                    and m.[Status] = 1
	                    and m.[MemberType] = 2
                    join [Users].[In_Owner] ino
	                    on ino.InstanceID = m.InstanceID
	                    and ino.ShellID = (select dbl.ShellID from [Accounts].[Shell].[DBLookUp] dbl where dbl.ShellName = 'Prolifiq')
                    where c.TokenID = @AccessTokenID
	                    and c.ApplicationPID = @ApplicationPID              
                ";

                var res = connection.Query<GetMemberByAccessToken_QRM>(sql, new {
                    AccessTokenID = query.AccessTokenID,
                    ApplicationPID = Globals.ApplicationPID
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
