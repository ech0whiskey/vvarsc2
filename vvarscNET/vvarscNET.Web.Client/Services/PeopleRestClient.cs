using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;
using RestSharp;
using System.Web;
using Newtonsoft.Json;
using vvarscNET.Model.Result;

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

        public IEnumerable<Member> ListMembersForOrganization(HttpContextBase Context, int OrganizationID)
        {
            var request = new RestRequest("organizations/{id}/members", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", OrganizationID, ParameterType.UrlSegment);

            var response = _client.Execute<List<Member>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<IEnumerable<Member>>(response.Content);
        }

        public Result CreateMember(HttpContextBase Context, Member member)
        {
            var request = new RestRequest("members", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddBody(member);

            var response = _client.Execute<List<Member>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }

        public Member GetMemberByID(HttpContextBase Context, int memberID)
        {
            var request = new RestRequest("members/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", memberID, ParameterType.UrlSegment);

            var response = _client.Execute<List<Member>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Member>(response.Content);
        }
    }
}