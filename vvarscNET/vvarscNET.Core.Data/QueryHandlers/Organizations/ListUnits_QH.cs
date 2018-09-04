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
    public class ListUnits_QH : IQueryHandler<ListUnits_Q, List<Unit>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListUnits_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<Unit> Handle(string accessTokenID, ListUnits_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    u.ID
	                    ,u.ParentUnitID
	                    ,u.UnitName
	                    ,u.UnitFullName
	                    ,u.UnitDesignation
	                    ,u.UnitDescription
	                    ,u.UnitCallsign
	                    ,u.IsHidden
	                    ,u.IsActive
	                    ,u.CreatedOn
	                    ,u.CreatedBy
	                    ,u.ModifiedOn
	                    ,u.ModifiedBy
                    from Organizations.Units u
                    where u.IsActive = 1
                ";

                var res = connection.Query<Unit>(sql).ToList();

                return res;
            }
        }
    }
}
