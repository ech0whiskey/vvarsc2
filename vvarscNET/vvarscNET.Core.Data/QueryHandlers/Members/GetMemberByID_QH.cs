using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.People;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class GetMemberByID_QH : IQueryHandler<GetMemberByID_Q, Member>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetMemberByID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Member Handle(string accessTokenID, GetMemberByID_Q query)
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
                        ,m.UserType
	                    ,m.IsActive
	                    ,m.CreatedOn
	                    ,m.CreatedBy
	                    ,m.ModifiedOn
	                    ,m.ModifiedBy
                    from [People].[Members] m
                    where m.ID = @MemberID              
                ";

                var res = connection.Query<Member>(sql, new { MemberID = query.MemberID }).FirstOrDefault();

                return res;
            }
        }
    }
}
