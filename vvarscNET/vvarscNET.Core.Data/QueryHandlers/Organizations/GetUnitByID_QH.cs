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
    public class GetUnitByID_QH : IQueryHandler<GetUnitByID_Q, Unit>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetUnitByID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Unit Handle(string accessTokenID, GetUnitByID_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    ;with units as (
	                    select
		                    u.*
	                    from [Organizations].Units u
	                    where u.ID = @UnitID
	                    union all
	                    select
		                    u.*
	                    from [Organizations].Units u
	                    join units tu
		                    on tu.ID = u.ParentUnitID
                    )

                    select distinct
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
                    from units u
                ";

                var flatResult = connection.Query<Unit>(sql, new { UnitID = query.ID }).ToList();

                return BuildTree(flatResult, query.ID).FirstOrDefault();
            }
        }

        private static IEnumerable<Unit> BuildTree(List<Unit> units, int queryID)
        {
            units.ForEach(i => i.ChildUnits = units.Where(a => a.ParentUnitID == i.ID).ToList());
            return units.Where(a => a.ID == queryID);
        }
    }
}
