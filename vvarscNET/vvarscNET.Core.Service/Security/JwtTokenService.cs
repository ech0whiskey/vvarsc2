using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Model.ResponseModels.Operations;
using vvarscNET.Model.Security;
using JWT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace vvarscNET.Core.Service.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly IClientRegistrationQueryService _clientRegService;

        public JwtTokenService(IClientRegistrationQueryService clientRegService)
        {
            _clientRegService = clientRegService;
        }

        public string EncodeJsonWebToken(JwtApiToken jwt, string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentNullException(nameof(privateKey));

            var payload = jwt.ToPayload();
            return JsonWebToken.Encode(payload, privateKey, JwtHashAlgorithm.HS512);
        }

        public JwtApiToken GetJsonWebToken(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException(nameof(jwt));

            return JwtApiToken.FromPayload(GetPayload(jwt));
        }

        public bool ValidateTokenHash(string jwt)
        {
            if (string.IsNullOrEmpty(jwt) || string.IsNullOrEmpty(jwt))
                throw new ArgumentNullException("Invalid jwt!");

            var payload = GetPayload(jwt);

            object app;
            payload.TryGetValue("App", out app);
            if (app == null) throw new SecurityException("Invalid App Name for Token");

            ClientRegistration result = _clientRegService.GetClientRegistrationByAppName(Globals.AuthHandlerToken, Convert.ToString(app));

            if (result == null)
                throw new SecurityException("Invalid Client Registration");

            if (string.IsNullOrEmpty(result.PrivateKey))
                throw new ArgumentNullException("Invalid PrivateKey!");
            
            var token = JsonWebToken.DecodeToObject(jwt, result.PrivateKey) as IDictionary<string, object>;

            return true;
        }

        private Dictionary<string, object> GetPayload(string jwt)
        {
            var segments = jwt.Trim().Split('.');
            if (segments.Length != 3)
                throw new UnauthorizedAccessException("Invalid JWT");

            var byteArray = JsonWebToken.Base64UrlDecode(segments[1]);
            var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(byteArray));

            return payload;
        }
    }
}