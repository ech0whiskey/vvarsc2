using vvarscNET.Core.Interfaces;
using vvarscNET.Core.QueryModels.Operations;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Model.ResponseModels.Operations;
using System;

namespace vvarscNET.Core.Service.QueryServices.Operations
{
    public class ClientRegistrationQueryService : IClientRegistrationQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public ClientRegistrationQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public ClientRegistration GetClientRegistrationByAppName(string accessToken, string app)
        {
            if (string.IsNullOrEmpty(app)) throw new ArgumentNullException(nameof(app));

            var regQry = new GetClientRegistrationByAppName_Q
            {
                appName = app
            };

            /// Container is not registering the IQueryHandler 
            /// ToDo: fix the IQueryHandler registration
            // _queryDispatcher.Dispatch<GetClientRegistrationByAppNameQuery, ClientRegistration>(accessToken, regQry);

            return new ClientRegistration() { AppName = app, IsActive = true, PrivateKey = "secret", ErrorReturnURL = "errorUrl", ReturnURL = "returnUrl" };
        }
    }
}
