using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;

namespace vvarscNET.Core.QueryModels.Authentication
{
    public class GetAccessTokenByPID_Q : IQuery<GetAccessToken_QRM>
    {
        public string AccessTokenID;
    }
}
