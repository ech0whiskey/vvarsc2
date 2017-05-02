using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.Service.QueryServices
{
    public class OrganizationQueryService : IOrganizationQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public OrganizationQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<Organization> ListOrganizations(string accessToken)
        {
            var query = new ListOrganizations_Q();

            var result = _queryDispatcher.Dispatch<ListOrganizations_Q, List<Organization>>(accessToken, query);

            return result;
        }

        public Organization GetOrganizationByID(string accessToken, int organizationID)
        {
            var query = new GetOrganizationByID_Q
            {
                ID = organizationID
            };

            var result = _queryDispatcher.Dispatch<GetOrganizationByID_Q, Organization>(accessToken, query);

            return result;
        }
    }
}
