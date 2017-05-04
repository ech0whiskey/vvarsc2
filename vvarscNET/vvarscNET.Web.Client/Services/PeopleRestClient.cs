using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Model.ResponseModels.People;
using RestSharp;
using System.Web;
using Newtonsoft.Json;

namespace vvarscNET.Web.Client.Services
{
    public class PeopleRestClient : IPeopleRestClient
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["webapibaseurl"];

        public PeopleRestClient()
        {
            _client = new RestClient(_url);
        }

        public IEnumerable<ListRanks_QRM> ListRanks(HttpContextBase Context)
        {
            var request = new RestRequest("ranks", Method.GET) { RequestFormat = DataFormat.Json};
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());

            var response = _client.Execute<List<ListRanks_QRM>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<IEnumerable<ListRanks_QRM>>(response.Content);
        }
    }
}