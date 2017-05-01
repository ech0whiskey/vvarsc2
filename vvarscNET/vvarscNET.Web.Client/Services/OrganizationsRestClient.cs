using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Model.Objects;
using RestSharp;
using System.Web;
using Newtonsoft.Json;

namespace vvarscNET.Web.Client.Services
{
    public class OrganizationsRestClient : IOrganizationsRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];

        public OrganizationsRestClient()
        {
            _client = new RestClient(_url);
        }

        public IEnumerable<Organization> ListOrganizations(HttpContextBase Context)
        {
            var request = new RestRequest("organizations", Method.GET) { RequestFormat = DataFormat.Json};
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());

            var response = _client.Execute<List<Organization>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<IEnumerable<Organization>>(response.Content);
        }
    }
}