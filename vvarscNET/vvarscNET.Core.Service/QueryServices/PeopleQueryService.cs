using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;
using vvarscNET.Core.QueryModels.People;

namespace vvarscNET.Core.Service.QueryServices
{
    public class PeopleQueryService : IPeopleQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PeopleQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<Member> ListMembersForOrganization(string accessToken, int organizationID)
        {
            var query = new ListMembersForOrganization_Q
            {
                OrganizationID = organizationID.ToString()
            };

            var result = _queryDispatcher.Dispatch<ListMembersForOrganization_Q, List<Member>>(accessToken, query);

            return result;
        }

        public List<ListRanks_QRM> ListRanks(string accessToken)
        {
            var query = new ListRanks_Q();

            var result = _queryDispatcher.Dispatch<ListRanks_Q, List<ListRanks_QRM>>(accessToken, query);

            return result;
        }
    }
}
