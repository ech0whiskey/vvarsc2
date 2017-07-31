using System;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller Repsponsible for Managing Organization Objects
    /// </summary>
    public class OrganizationsController : ApiController
    {
        private IOrganizationQueryService _orgQueryService;
        private IOrganizationCommandService _orgCmdService;

        /// <summary>
        /// Constructor for Organizations Controller
        /// </summary>
        /// <param name="orgQueryService"></param>
        /// <param name="orgCmdService"></param>
        public OrganizationsController(IOrganizationQueryService orgQueryService, IOrganizationCommandService orgCmdService)
        {
            _orgQueryService = orgQueryService;
            _orgCmdService = orgCmdService;
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
            var returnData = _orgQueryService.ListOrganizations(Request.GetUserContext().AccessToken);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to Get a Specific Organizations
        /// </summary>
        /// <param name="id">ID of Organization</param>
        /// <returns>Organization</returns>
        [HttpGet]
        [Route("organizations/{id}")]
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetOrganizationByID([FromUri] int id)
        {
            var returnData = _orgQueryService.GetOrganizationByID(Request.GetUserContext().AccessToken, id);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to get list of Organization Roles
        /// </summary>
        /// <param name="id">ID of OrgRole</param>
        /// <returns>List of OrgRole</returns>
        [HttpGet]
        [Route("organizations/{id}/roles")]
        [ResponseType(typeof(List<OrgRole>))]
        public IHttpActionResult ListRolesForOrganization([FromUri] int id)
        {
            var returnData = _orgQueryService.ListRolesForOrganization(Request.GetUserContext().AccessToken, id);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to get a specific Organization Role
        /// </summary>
        /// <param name="id">ID of OrgRole</param>
        /// <returns>OrgRole</returns>
        [HttpGet]
        [Route("roles/{id}")]
        [ResponseType(typeof(OrgRole))]
        public IHttpActionResult GetOrgRoleByID([FromUri] int id)
        {
            var returnData = _orgQueryService.GetOrgRoleByID(Request.GetUserContext().AccessToken, id);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to Update Organization Role
        /// </summary>
        /// <param name="id">ID of OrgRole</param>
        /// <param name="role">OrgRole Object to Update</param>
        /// <returns>Result</returns>
        [HttpPut]
        [Route("roles/{id}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult UpdateOrgRole([FromUri] int id, [FromBody] OrgRole role)
        {
            //Ensure that Role Object in Route is the same as Body
            if (id != role.ID)
                return BadRequest();

            var result = _orgCmdService.UpdateOrgRole(Request.GetUserContext(), role);

            return Ok(result);
        }
    }
}
