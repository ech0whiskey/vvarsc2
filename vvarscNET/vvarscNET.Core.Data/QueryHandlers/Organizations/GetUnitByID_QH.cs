using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using System;
using System.Linq;
using System.Data.SqlClient;

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
                    where u.ID = @UnitID
                ";

                var res = connection.Query<Unit>(sql, new { UnitID = query.ID }).FirstOrDefault();

                return res;
            }
        }
    }
}
