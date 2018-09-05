using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.Organizations
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
                    from Organizations.PayGrades pg
                    order by
	                    pg.PayGradeOrderBy        
                ";

                var res = connection.Query<PayGrade>(sql).ToList();

                return res;
            }
        }
    }
}
