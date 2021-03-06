﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Model.Objects.Organizations;
using RestSharp;
using System.Web;
using Newtonsoft.Json;
using vvarscNET.Model.Result;

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

        public Organization GetOrganizationByID(HttpContextBase Context, int OrganizationID)
        {
            var request = new RestRequest("organizations/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", OrganizationID, ParameterType.UrlSegment);

            var response = _client.Execute<Organization>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Organization>(response.Content);
        }

        public List<OrgRole> ListRolesForOrganization(HttpContextBase Context, int OrganizationID)
        {
            var request = new RestRequest("organizations/{id}/roles", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", OrganizationID, ParameterType.UrlSegment);

            var response = _client.Execute<List<OrgRole>>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<List<OrgRole>>(response.Content);
        }

        public OrgRole GetOrgRoleByID(HttpContextBase Context, int RoleID)
        {
            var request = new RestRequest("roles/{id}", Method.GET) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", RoleID, ParameterType.UrlSegment);

            var response = _client.Execute<OrgRole>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<OrgRole>(response.Content);
        }

        public Result EditOrgRole(HttpContextBase Context, OrgRole Role)
        {
            var request = new RestRequest("roles/{id}", Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddHeader("Authorization", "access " + Context.Session["AccessToken"].ToString());
            request.AddParameter("id", Role.ID, ParameterType.UrlSegment);
            request.AddBody(Role);

            var response = _client.Execute<Result>(request);

            if (response.Content == null)
                throw new Exception(response.ErrorMessage);

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }
    }
}