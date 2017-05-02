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
    public class GetOrganizationByID_QH : IQueryHandler<GetOrganizationByID_Q, Organization>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetOrganizationByID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Organization Handle(string accessTokenID, GetOrganizationByID_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    o.ID
	                    ,o.OrganizationName
	                    ,o.OrganizationSpectrumID
	                    ,o.OrganizationWebsiteURL
	                    ,o.IsActive
	                    ,o.CreatedOn
	                    ,o.CreatedBy
	                    ,o.ModifiedOn
	                    ,o.ModifiedBy
                    from [Organizations].[Organizations] o
                    where o.ID = @ID
                ";

                var res = connection.Query<Organization>(sql, new { ID = query.ID }).FirstOrDefault();

                return res;
            }
        }
    }
}
