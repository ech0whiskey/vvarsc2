using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.ResponseModels.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class ListRanks_QH : IQueryHandler<ListRanks_Q, List<ListRanks_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListRanks_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListRanks_QRM> Handle(string accessTokenID, ListRanks_Q query)
        {
            Dictionary<int, ListRanks_QRM> xRanks = new Dictionary<int, ListRanks_QRM>();
            Dictionary<int, HashSet<OrgRole>> xRanksOrgRoles = new Dictionary<int, HashSet<OrgRole>>();

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    r.ID [RankID]
	                    ,r.PayGradeID
	                    ,pg.PayGradeName
	                    ,pg.PayGradeDisplayName
	                    ,pg.PayGradeOrderBy
	                    ,pg.PayGradeGroup
                        ,pg.PayGradeDescriptionText
                        ,pg.PayGradeNotes
	                    ,r.RankName
	                    ,r.RankAbbr
	                    ,r.RankType
	                    ,r.RankImage
	                    ,r.RankGroupName
	                    ,r.RankGroupImage
                    from People.Ranks r
                    join People.PayGrades pg
	                    on pg.ID = r.PayGradeID
                    order by
	                    pg.PayGradeOrderBy        
                ";

                var res = connection.Query<ListRanks_QRM>(sql).ToList();

                return res;
            }
        }
    }
}
