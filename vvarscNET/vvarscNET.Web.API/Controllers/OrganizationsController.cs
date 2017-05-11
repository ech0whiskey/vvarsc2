using System;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.Organizations;
//
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller Repsponsible for Managing Organization Objects
    /// </summary>
    public class OrganizationsController : ApiController
    {
        private IOrganizationQueryService _orgService;

        /// <summary>
        /// Constructor for Organizations Controller
        /// </summary>
        /// <param name="orgService"></param>
        public OrganizationsController(IOrganizationQueryService orgService)
        {
            _orgService = orgService;
        }

        /// <summary>
        /// Method to List all Organizations
        /// </summary>
        /// <returns>List of Organizations</returns>
        [HttpGet]
        [Route("organizations")]
        [ResponseType(typeof(List<Organization>))]
        public IHttpActionResult ListOrganizations()
        {
            var returnData = _orgService.ListOrganizations(Request.GetUserContext().AccessToken);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to Get a Specific Organizations
        /// </summary>
        /// <returns>Organization</returns>
        [HttpGet]
        [Route("organizations/{id}")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetOrganizationByID([FromUri] int id)
        {
            var returnData = _orgService.GetOrganizationByID(Request.GetUserContext().AccessToken, id);

            return Ok(returnData);
        }

    }
}
