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
        /// <returns>List of OrgRole</returns>
        [HttpGet]
        [Route("roles")]
        [ResponseType(typeof(List<OrgRole>))]
        public IHttpActionResult ListRoles()
        {
            var returnData = _orgQueryService.ListRoles(Request.GetUserContext().AccessToken);

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

        /// <summary>
        /// Method to Retrieve List of Units
        /// </summary>
        /// <returns>Unit</returns>
        [HttpGet]
        [Route("units")]
        [ResponseType(typeof(List<Unit>))]
        public IHttpActionResult ListUnits()
        {
            var result = _orgQueryService.ListUnits(Request.GetUserContext().AccessToken);

            return Ok(result);
        }

        /// <summary>
        /// Method to Retrive Single Unit
        /// </summary>
        /// <param name="id">ID of Unit</param>
        /// <param name="includeChildren">If set to true, child units will be included in the returned object.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("units/{id}")]
        [ResponseType(typeof(Unit))]
        public IHttpActionResult GetUnitByID([FromUri] int id, [FromUri] bool includeChildren)
        {
            var result = _orgQueryService.GetUnitByID(Request.GetUserContext().AccessToken, id, includeChildren);

            return Ok(result);
        }

        /// <summary>
        /// Method to Create a new Unit
        /// </summary>
        /// <param name="unit">Unit Object to Create</param>
        /// <returns></returns>
        [HttpPost]
        [Route("units")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult CreateUnit([FromBody] Unit unit)
        {
            var result = _orgCmdService.CreateUnit(Request.GetUserContext(), unit);

            return Ok(result);
        }

        /// <summary>
        /// Method to Update Unit
        /// </summary>
        /// <param name="id">ID of Unit</param>
        /// <param name="unit">Unit Object</param>
        /// <returns></returns>
        [HttpPut]
        [Route("units/{id}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult UpdateUnit([FromUri] int id, [FromBody] Unit unit)
        {
            //Ensure that Unit Object in Route is the same as Body
            if (id != unit.ID)
                return BadRequest();

            var result = _orgCmdService.UpdateUnit(Request.GetUserContext(), unit);

            return Ok(result);
        }

        /// <summary>
        /// Method To Delete Unit
        /// </summary>
        /// <param name="id">ID of Unit</param>
        /// <param name="deleteChildren">If set to true, child units will be deleted.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("units/{id}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult DeleteUnit([FromUri] int id, [FromUri] bool deleteChildren)
        {
            Result result;

            if (deleteChildren)
                result = _orgCmdService.DeleteUnit(Request.GetUserContext(), id);
            else
                result = _orgCmdService.DeleteUnitRecursive(Request.GetUserContext(), id);

            return Ok(result);
        }
    }
}
