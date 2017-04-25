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
    public class GetMemberByPID_QH : IQueryHandler<GetMemberByPID_Q, GetMemberByPID_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetMemberByPID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetMemberByPID_QRM Handle(string accessTokenID, GetMemberByPID_Q query)
        {
            if (string.IsNullOrEmpty(query.MemberPID))
                throw new ArgumentNullException(nameof(query.MemberPID));

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
                        ,m.Language
                        ,m.Region
                        ,m.TimeZone
                    from Core.Users.Members m
                    where m.MemberPID = @MemberPID               
                ";

                var res = connection.Query<GetMemberByPID_QRM>(sql, new { MemberPID = query.MemberPID }).FirstOrDefault();

                return res;
            }
        }
    }
}
