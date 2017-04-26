using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Security;
using vvarscNET.Web.API.OwinMiddlewares.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System.Security;
using System.Security.Claims;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace vvarscNET.Web.API.OwinMiddlewares.Handler
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationMiddlewareHandler : AuthenticationHandler<Configuration.ApiAuthenticationOptions>
    {
        private readonly IAuthenticationService _authService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public AuthenticationMiddlewareHandler(Configuration.ApiAuthenticationOptions options)
        {
            _authService = options.AuthService;
        }

        /// <summary>
        /// The core authentication logic which must be provided by the handler. Will be invoked at most
        /// once per request. Do not call directly, call the wrapping Authenticate method instead.
        /// </summary>
        /// <returns>
        /// The ticket data provided by the authentication logic
        /// </returns>
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authHeader = Request.Headers["Authorization"];
            var tokenType = "Access ";
            if (authHeader == null || !authHeader.StartsWith(tokenType, System.StringComparison.CurrentCultureIgnoreCase))
                return Task.FromResult(new AuthenticationTicket(null, null));

            var accessTokenID = authHeader.Substring(tokenType.Length).Trim();

            Context.Response.Headers.Add("Authorization", new[] { $"{tokenType}{accessTokenID}" });

            var token = _authService.GetAccessTokenByValue(accessTokenID);
            if (token != null)
            {
                var identity = new ClaimsIdentity(Options.SignInAsAuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, token.MemberID, null, Options.AuthenticationType));
                identity.AddClaim(new Claim(ClaimTypes.Sid, token.ID, null, Options.AuthenticationType));

                return Task.FromResult(new AuthenticationTicket(identity, new AuthenticationProperties()));
            }
            else
                throw new AuthenticationException("Unable to Retrive AccessToken");
        }

        /// <summary>
        /// Called once by common code after initialization. If an authentication middleware responds directly to
        /// specifically known paths it must override this virtual, compare the request path to it's known paths, 
        /// provide any response information as appropriate, and true to stop further processing.
        /// </summary>
        /// <returns>
        /// Returning false will cause the common code to call the next middleware in line. Returning true will
        /// cause the common code to begin the async completion journey without calling the rest of the middleware
        /// pipeline.
        /// </returns>
        public override async Task<bool> InvokeAsync()
        {
            var ticket = await AuthenticateAsync();

            if (ticket == null) return false;
            Context.Authentication.SignIn(ticket.Properties, ticket.Identity);
            return await base.InvokeAsync();
        }
    }
}