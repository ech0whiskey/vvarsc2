using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.People;

namespace vvarscNET.Core.QueryModels.People
{
    public class GetMemberByAccessToken_Q : IQuery<GetMemberByAccessToken_QRM>
    {
        public string AccessToken;
    }
}
