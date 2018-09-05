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
    /// Controller Repsponsible for Managing Unit Objects
    /// </summary>
    public class UnitsController : ApiController
    {
        private IUnitQueryService _unitQueryService;
        private IUnitCommandService _unitCmdService;

        /// <summary>
        /// Constructor for Units Controller
        /// </summary>
        /// <param name="unitQueryService"></param>
        /// <param name="unitCmdService"></param>
        public UnitsController(IUnitQueryService unitQueryService, IUnitCommandService unitCmdService)
        {
            _unitQueryService = unitQueryService;
            _unitCmdService = unitCmdService;
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
            var result = _unitQueryService.ListUnits(Request.GetUserContext().AccessToken);

            return Ok(result);
        }

        /// <summary>
        /// Method to Retrive Single Unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <param name="includeChildren">If set to true, child units will be included in the returned object.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("units/{unitID}")]
        [ResponseType(typeof(Unit))]
        public IHttpActionResult GetUnitByID([FromUri] int unitID, [FromUri] bool includeChildren)
        {
            var result = _unitQueryService.GetUnitByID(Request.GetUserContext().AccessToken, unitID, includeChildren);

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
            var result = _unitCmdService.CreateUnit(Request.GetUserContext(), unit);

            return Ok(result);
        }

        /// <summary>
        /// Method to Update Unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <param name="unit">Unit Object</param>
        /// <returns></returns>
        [HttpPut]
        [Route("units/{unitID}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult UpdateUnit([FromUri] int unitID, [FromBody] Unit unit)
        {
            //Ensure that Unit Object in Route is the same as Body
            if (unitID != unit.ID)
                return BadRequest();

            var result = _unitCmdService.UpdateUnit(Request.GetUserContext(), unit);

            return Ok(result);
        }

        /// <summary>
        /// Method To Delete Unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <param name="deleteChildren">If set to true, child units will be deleted.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("units/{unitID}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult DeleteUnit([FromUri] int unitID, [FromUri] bool deleteChildren)
        {
            Result result;

            if (deleteChildren)
                result = _unitCmdService.DeleteUnit(Request.GetUserContext(), unitID);
            else
                result = _unitCmdService.DeleteUnitRecursive(Request.GetUserContext(), unitID);

            return Ok(result);
        }

        /// <summary>
        /// Method to List Organization Roles for a Unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <returns></returns>
        [HttpGet]
        [Route("units/{unitID}/orgRoles")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult ListOrgRolesForUnit([FromUri] int unitID)
        {
            var result = _unitQueryService.ListOrgRolesForUnit(Request.GetUserContext().AccessToken, unitID);

            return Ok(result);
        }

        /// <summary>
        /// Method to Add Organization Role to unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <param name="unitOrgRole">Organization Role Object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("units/{unitID}/orgRoles")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult AddOrgRoleToUnit([FromUri] int unitID, [FromBody] UnitOrgRole unitOrgRole)
        {
            //Ensure that Unit Object in Route is the same as Body
            if (unitID != unitOrgRole.UnitID)
                return BadRequest();

            var result = _unitCmdService.AddOrgRoleToUnit(Request.GetUserContext(), unitID, unitOrgRole);

            return Ok(result);
        }

        /// <summary>
        /// Method to Remove Organization Role from Unit
        /// </summary>
        /// <param name="unitID">ID of Unit</param>
        /// <param name="orgRoleID">ID of Organization Role</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("units/{unitID}/orgRoles/{orgRoleID}")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult RemoveOrgRoleFromUnit([FromUri] int unitID, [FromUri] int orgRoleID)
        {
            var result = _unitCmdService.RemoveOrgRoleFromUnit(Request.GetUserContext(), unitID, orgRoleID);

            return Ok(result);
        }
    }
}
