using System.Collections.Generic;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;
using System.Web;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.Client.Interfaces
{
    public interface IPeopleRestClient
    {
        IEnumerable<ListRanks_QRM> ListRanks(HttpContextBase Context);
        IEnumerable<Member> ListMembersForOrganization(HttpContextBase Context, int OrganizationID);
        Result CreateMember(HttpContextBase Context, Member member);
        Member GetMemberByID(HttpContextBase Context, int memberID);
    }
}
