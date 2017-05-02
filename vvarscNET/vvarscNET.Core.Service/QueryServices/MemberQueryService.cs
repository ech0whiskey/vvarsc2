﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Core.QueryModels.People;

namespace vvarscNET.Core.Service.QueryServices
{
    public class MemberQueryService : IMemberQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public MemberQueryService(IQueryDispatcher queryDispatcher)
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
    }
}
