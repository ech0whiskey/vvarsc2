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
        private IOrganizationQueryService _orgQueryService;
        private IOrganizationCommandService _orgCmdService;

        /// <summary>
        /// Constructor for Units Controller
        /// </summary>
        /// <param name="orgQueryService"></param>
        /// <param name="orgCmdService"></param>
        public UnitsController(IOrganizationQueryService orgQueryService, IOrganizationCommandService orgCmdService)
        {
            _orgQueryService = orgQueryService;
            _orgCmdService = orgCmdService;
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
