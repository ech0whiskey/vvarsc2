using Dapper;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.Authentication;
using System;
using System.Linq;

namespace vvarscNET.Core.Data.QueryHandlers.Authentication
{
    public class AuthenticateMember_QH : IPermissionQueryHandler<AuthenticateMember_Q, AuthenticateMember_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public AuthenticateMember_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public AuthenticateMember_QRM Handle(AuthenticateMember_Q query)
        {
            if (string.IsNullOrEmpty(query.UserName))
                throw new ArgumentNullException(nameof(query.UserName));

            if (string.IsNullOrEmpty(query.Password))
                throw new ArgumentNullException(nameof(query.Password));

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    CAST(m.InstanceID as nvarchar(100)) [InstanceID]
	                    ,m.MemberPID
	                    ,ino.OwnerID [MemberID]
	                    ,m.[Login] [UserName]
	                    ,m.[Email]
	                    ,m.FirstName
	                    ,m.LastName
	                    ,m.LastLoginDate
	                    ,m.[Language]
	                    ,m.Region
	                    ,m.TimeZone
	                    ,m.Customer_ID
	                    ,ino.ShellID
	                    ,m.InsertDate [CreatedOn]
                    from Users.Members m
                    join Users.In_Owner ino
	                    on ino.InstanceID = m.InstanceID
	                    and ino.ShellID = (select dbl.ShellID from Accounts.Shell.DBLookUp dbl where dbl.ShellName = 'prolifiq')
                    where m.[Login] = @UserName
	                    and m.[Password] = @Password
	                    and m.MemberType = 2
	                    and m.[Status] = 1
                ";

                var res = connection.Query<AuthenticateMember_QRM>(sql, new
                {
                    UserName = query.UserName,
                    Password = query.Password
                }).FirstOrDefault();

                return res;
            }
        }
    }
}
