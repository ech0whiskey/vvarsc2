using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.Organizations
{
    public class ListOrganizations_QH : IQueryHandler<ListOrganizations_Q, List<Organization>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListOrganizations_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<Organization> Handle(string accessTokenID, ListOrganizations_Q query)
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
                ";

                var res = connection.Query<Organization>(sql).ToList();

                return res;
            }
        }
    }
}
