using vvarscNET.Core.Service.Interfaces;
using Microsoft.Owin.Security;
using SimpleInjector;

namespace vvarscNET.Web.API.OwinMiddlewares.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiAuthenticationOptions : AuthenticationOptions
    {
        private readonly Container _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ApiAuthenticationOptions(Container container) : base(ApiKeyDefaults.AuthenticationType)
        {
            _container = container;
            AuthenticationMode = AuthenticationMode.Active;
            SignInAsAuthenticationType = ApiKeyDefaults.AuthenticationType;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SignInAsAuthenticationType { get; private set; }

        /// <summary>
        /// Get Token Service
        /// </summary>
        public IAuthenticationService AuthService { get { return _container.GetInstance<IAuthenticationService>(); } }
    }
}