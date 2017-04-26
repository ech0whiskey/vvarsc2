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
    public class GetMemberByID_QH : IQueryHandler<GetMemberByID_Q, GetMemberByID_QRM>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetMemberByID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public GetMemberByID_QRM Handle(string accessTokenID, GetMemberByID_Q query)
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

                var res = connection.Query<GetMemberByID_QRM>(sql, new { MemberID = query.MemberID }).FirstOrDefault();

                return res;
            }
        }
    }
}
