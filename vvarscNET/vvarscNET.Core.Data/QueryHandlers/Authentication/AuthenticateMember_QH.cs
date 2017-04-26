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
                    join [Authentication].[Credentials] c
                        on c.MemberID = m.ID
	                    and c.[PasswordHash] = @Password
                    where m.IsActive = 1
	                    and m.UserName = @UserName
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
