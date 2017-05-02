using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Core.QueryModels.People
{
    public class GetMemberByAccessToken_Q : IQuery<Member>
    {
        public string AccessToken;
    }
}
