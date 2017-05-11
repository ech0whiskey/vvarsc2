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
	                    ,pg.ID [PayGradeID]
	                    ,pg.PayGradeName
	                    ,pg.PayGradeDisplayName
	                    ,pg.PayGradeGroup
	                    ,r1.ID [RankID]
	                    ,r1.RankName
	                    ,r1.RankAbbr
	                    ,r1.RankType
	                    ,r1.RankImage
	                    ,r1.RankGroupName
	                    ,r1.RankGroupImage
                    from People.Members m
                    left join People.Ranks r1
	                    on r1.ID = m.RankID
                    left join People.PayGrades pg
	                    on pg.ID = r1.PayGradeID
                    where m.ID = @MemberID              
                ";

                var res = connection.Query<Member>(sql, new { MemberID = query.MemberID }).FirstOrDefault();

                return res;
            }
        }
    }
}
