using vvarscNET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

/// <summary>
/// HttpRequest Message Extensions
/// </summary>
public static class HttpRequestMessageExtension
{
    /// <summary>
    /// Method to Get UserContext from HttpRequest
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static UserContext GetUserContext(this HttpRequestMessage request)
    {
        IIdentity requestIdentity = request.GetRequestContext().Principal.Identity;
        IEnumerable<Claim> requestClaims = ((ClaimsIdentity)requestIdentity).Claims;

        var AccessToken = requestClaims
                            .Where(c => c.Type.Equals(ClaimTypes.Sid, StringComparison.CurrentCultureIgnoreCase))
                            .FirstOrDefault(c => c != null);

        var MemberID = requestClaims
                            .Where(c => c.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.CurrentCultureIgnoreCase))
                            .FirstOrDefault(c => c != null);

        UserContext context = new UserContext
        {
            AccessToken = AccessToken.Value,
            MemberID = Convert.ToInt32(MemberID.Value)
        };

        return context;
    }
}