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

        public IEnumerable<PayGrade> ListPayGrades(HttpContextBase Context)
        {
            var request = new RestRequest("paygrades", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());

            var response = _client.Execute<List<PayGrade>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<IEnumerable<PayGrade>>(response.Content);
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

        public Result CreateMember(HttpContextBase context, Member member)
        {
            var request = new RestRequest("members", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + context.Session["AccessToken"].ToString());
            request.AddBody(member);

            var response = _client.Execute<List<Member>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }

        public Member GetMemberByID(HttpContextBase context, int memberID)
        {
            var request = new RestRequest("members/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + context.Session["AccessToken"].ToString());
            request.AddParameter("id", memberID, ParameterType.UrlSegment);

            var response = _client.Execute<List<Member>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Member>(response.Content);
        }

        public Result EditMember(HttpContextBase context, Member member)
        {
            var request = new RestRequest("members/{id}", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + context.Session["AccessToken"].ToString());
            request.AddParameter("id", member.ID, ParameterType.UrlSegment);
            request.AddBody(member);

            var response = _client.Execute<Result>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }

        public Result DeleteMember(HttpContextBase context, int memberID)
        {
            var request = new RestRequest("members/{id}", Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + context.Session["AccessToken"].ToString());
            request.AddParameter("id", memberID, ParameterType.UrlSegment);

            var response = _client.Execute<Result>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }
    }
}