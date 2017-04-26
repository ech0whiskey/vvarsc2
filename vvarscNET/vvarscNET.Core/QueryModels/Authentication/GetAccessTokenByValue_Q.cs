using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;

namespace vvarscNET.Core.QueryModels.Authentication
{
    public class GetAccessTokenByValue_Q : IQuery<GetAccessToken_QRM>
    {
        public string AccessToken;
    }
}
