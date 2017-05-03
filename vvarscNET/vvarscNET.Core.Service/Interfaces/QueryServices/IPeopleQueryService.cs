using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IPeopleQueryService
    {
        List<Member> ListMembersForOrganization(string accessToken, int organizationID);

        List<Rank_QRM> ListRanks(string accessToken);
    }
}
