using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.RequestModels.Authentication;
using vvarscNET.Model.Security;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateMember_QRM AuthenticateMember(string accessTokenID, AuthenticateMemberRequestModel model);

        AccessToken GenerateAccessToken(string memberPID, int shellID);

        AccessToken RenewAccessToken(string memberPID, int shellID, string parentTokenID);

        AccessToken GetAccessTokenByID(string accessTokenID);

        AccessToken GetLatestMemberAccessToken(string memberPID, int shellID);

        Result ExpireAccessToken(string memberPID, int shellID, string accessTokenID);
    }
}