using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class ListPayGrades_QH : IQueryHandler<ListPayGrades_Q, List<PayGrade>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListPayGrades_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<PayGrade> Handle(string accessTokenID, ListPayGrades_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    pg.ID
	                    ,pg.PayGradeName
	                    ,pg.PayGradeDisplayName
	                    ,pg.PayGradeOrderBy
	                    ,pg.PayGradeGroup
	                    ,pg.IsActive
                    from People.PayGrades pg
                    order by
	                    pg.PayGradeOrderBy        
                ";

                var res = connection.Query<PayGrade>(sql).ToList();

                return res;
            }
        }
    }
}
