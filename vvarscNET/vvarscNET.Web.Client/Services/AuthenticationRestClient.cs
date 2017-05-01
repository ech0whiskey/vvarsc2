using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using vvarscNET.Web.Client.Models;
using vvarscNET.Web.Client.Interfaces;
using RestSharp;
using vvarscNET.Model.Security;
using vvarscNET.Model.RequestModels.Authentication;

namespace vvarscNET.Web.Client.Services
{
    public class AuthenticationRestClient : IAuthenticationRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];

        public AuthenticationRestClient()
        {
            _client = new RestClient(_url);
        }

        public AccessToken Login(AuthenticateMemberRequestModel model)
        {
            var request = new RestRequest("authentication/login", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access 00055U8J7bJ4MkWv7x6lMi38Yg");
            request.AddBody(model);

            var response = _client.Execute<AccessToken>(request);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }

        public AccessToken Logout(LogoutMemberRequestModel model)
        {
            var request = new RestRequest("authentication/logout", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + model.AccessToken);
            request.AddBody(model);

            var response = _client.Execute<AccessToken>(request);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            return response.Data;
        }
    }
}