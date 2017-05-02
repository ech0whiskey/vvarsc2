using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.People;
using System.Collections.Generic;

namespace vvarscNET.Core.QueryModels.People
{
    public class ListMembersForOrganization_Q : IQuery<List<Member>>
    {
        public string OrganizationID;
    }
}
