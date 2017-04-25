using vvarscNET.Web.API.OwinMiddlewares.Configuration;
using vvarscNET.Web.API.OwinMiddlewares.Handler;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace vvarscNET.Web.API.OwinMiddlewares
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiKeyDefaults
    {
        /// <summary>
        /// 
        /// </summary>
        public const string AuthenticationType = "JwtToken";
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApiAuthenticationMiddleware : AuthenticationMiddleware<ApiAuthenticationOptions>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public ApiAuthenticationMiddleware(OwinMiddleware next, ApiAuthenticationOptions options)
            : base(next, options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override AuthenticationHandler<ApiAuthenticationOptions> CreateHandler()
        {
            return new AuthenticationMiddlewareHandler(Options);
        }
    }
}
