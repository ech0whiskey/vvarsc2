using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.RequestModels.Authentication;
using vvarscNET.Model.Security;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateMember_QRM AuthenticateMember(string accessToken, AuthenticateMemberRequestModel model);

        AccessToken GenerateAccessToken(int memberPID, int organizationID);

        AccessToken RenewAccessToken(int memberPID, int organizationID, string parentToken);

        AccessToken GetAccessTokenByValue(string accessToken);

        AccessToken GetLatestMemberAccessToken(int memberPID, int organizationID);

        Result ExpireAccessToken(int memberPID, int shellID, string accessToken);
    }
}