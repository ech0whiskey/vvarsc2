using System.Collections.Generic;
using vvarscNET.Web.Client.Models;
using vvarscNET.Model.Security;
using vvarscNET.Model.RequestModels.Authentication;

namespace vvarscNET.Web.Client.Interfaces
{
    public interface IAuthenticationRestClient
    {
        AccessToken Login(AuthenticateMemberRequestModel model);
        AccessToken Logout(LogoutMemberRequestModel model);
    }
}
