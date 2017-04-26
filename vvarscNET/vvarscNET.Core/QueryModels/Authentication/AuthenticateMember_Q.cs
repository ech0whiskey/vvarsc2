using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;

namespace vvarscNET.Core.QueryModels.Authentication
{
    public class AuthenticateMember_Q : IQuery<AuthenticateMember_QRM>
    {
        public string UserName;
        public string Password;
    }
}
