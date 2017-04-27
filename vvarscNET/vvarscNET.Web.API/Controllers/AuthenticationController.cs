using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Model.RequestModels.Authentication;
using vvarscNET.Model.Result;
using vvarscNET.Model.Security;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Handling Authentication of Members using AdminConsole System
    /// </summary>
    public class AuthenticationController : ApiController
    {
        private IAuthenticationService _authService;

        /// <summary>
        /// Constructor for Authentication Controller
        /// </summary>
        /// <param name="authService"></param>
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Method to Authenticate Member. This method will return an AccessToken Object encompassing the user details, signifying they should be able to login.
        /// </summary>
        /// <param name="model">AuthenticateMemberRequestModel</param>
        /// <returns>Member</returns>
        [HttpPost]
        [Route("authentication/login")]
        [ResponseType(typeof(AccessToken))]
        public IHttpActionResult ProcessLogin([FromBody] AuthenticateMemberRequestModel model)
        {
            //Check if Member can Be Authenticated
            var memResult = _authService.AuthenticateMember(Request.GetUserContext().AccessToken, model);

            if (memResult != null)
            {
                //See if Member already has a valid accessToken from this app
                var existingToken = _authService.GetLatestMemberAccessToken(memResult.ID, memResult.OrganizationID);

                //Token exists and is valid
                if (existingToken != null && existingToken.ValidTo > DateTime.UtcNow)
                {
                    return Ok(existingToken);
                }
                else
                {
                    //Generate AccessToken for Member
                    var tokenResult = _authService.GenerateAccessToken(memResult.ID, memResult.OrganizationID);

                    if (tokenResult != null)
                        return Ok(tokenResult);
                    else
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error in Creating AccessToken for Member"));
                }
            }
            else
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Member Not Authorized for AdminConsole Access"));
        }

        /// <summary>
        /// Method to renew AccessToken for Member - requires that the member has a currently valid token
        /// </summary>
        /// <param name="model">RenewTokenRequestModel</param>
        /// <returns></returns>
        [HttpPost]
        [Route("authentication/renew")]
        [ResponseType(typeof(AccessToken))]
        public IHttpActionResult RenewToken([FromBody] RenewTokenRequestModel model)
        {
            //Check AccessTokenID passed-in to make sure it's still valid
            var currentToken = _authService.GetAccessTokenByValue(model.AccessToken);

            //Token Exists
            if (currentToken != null)
            {
                //Make sure Token belongs to Member
                if (currentToken.MemberID == model.MemberID.ToString())
                {
                    //Make sure Token isn't Expired
                    if (currentToken.ValidTo > DateTime.UtcNow)
                    {
                        var newTokenResult = _authService.RenewAccessToken(model.MemberID, model.OrganizationID, model.AccessToken);
                        return Ok(newTokenResult);
                    }
                    else
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to renew Expired AccessToken - please try to login again."));
                }
                else
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Retrived AccessToken doesn't Match Member."));
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error - Unable to retrieve Member AccessToken for renewal."));
            }
        }

        /// <summary>
        /// Method to Logout Member - results in a call to DB to expire the Member's Current AccessToken
        /// </summary>
        /// <param name="model">LogoutMemberRequestModel</param>
        /// <returns>Member</returns>
        [HttpPost]
        [Route("authentication/logout")]
        [ResponseType(typeof(AccessToken))]
        public IHttpActionResult ProcessLogout([FromBody] LogoutMemberRequestModel model)
        {
            //call service to expire AccessToken for this Member
            var result = _authService.ExpireAccessToken(model.MemberID, model.OrganizationID, model.AccessToken);

            if (result != null)
                return Ok(result);
            else
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error in ExpireAccessToken."));
        }
    }
}