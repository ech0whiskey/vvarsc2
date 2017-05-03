using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IPeopleQueryService
    {
        List<Member> ListMembersForOrganization(string accessToken, int organizationID);
    }
}
