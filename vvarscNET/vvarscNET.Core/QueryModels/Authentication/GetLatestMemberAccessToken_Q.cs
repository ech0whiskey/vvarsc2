using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;

namespace vvarscNET.Core.QueryModels.Authentication
{
    public class GetLatestMemberAccessToken_Q : IQuery<GetAccessToken_QRM>
    {
        public int MemberID;
        public int OrganizationID;
    }
}
