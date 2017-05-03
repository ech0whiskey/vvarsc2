using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class ListRanks_QH : IQueryHandler<ListRanks_Q, List<Rank_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListRanks_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<Rank_QRM> Handle(string accessTokenID, ListRanks_Q query)
        {
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
	                    ,r.RankType
	                    ,r.RankName           
                ";

                var res = connection.Query<Rank_QRM>(sql).ToList();

                return res;
            }
        }
    }
}
