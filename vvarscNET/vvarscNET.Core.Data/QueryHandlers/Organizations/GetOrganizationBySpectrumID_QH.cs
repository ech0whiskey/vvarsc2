using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects;
using System;
using System.Linq;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.Organizations
{
    public class GetOrganizationBySpectrumID_QH : IQueryHandler<GetOrganizationBySpectrumID_Q, Organization>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetOrganizationBySpectrumID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public Organization Handle(string accessTokenID, GetOrganizationBySpectrumID_Q query)
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
                    where o.OrganizationSpectrumID = @SpectrumID
                ";

                var res = connection.Query<Organization>(sql, new { SpectrumID = query.SpectrumID }).FirstOrDefault();

                return res;
            }
        }
    }
}
