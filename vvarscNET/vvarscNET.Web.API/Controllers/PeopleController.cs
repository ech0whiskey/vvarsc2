using System;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller Repsponsible for Managing People and Related Objects
    /// </summary>
    public class PeopleController : ApiController
    {
        private IPeopleQueryService _peopleService;

        /// <summary>
        /// Constructor for People Controller
        /// </summary>
        /// <param name="peopleService"></param>
        public PeopleController(IPeopleQueryService peopleService)
        {
            _peopleService = peopleService;
        }

        /// <summary>
        /// Method to List all Ranks
        /// </summary>
        /// <returns>List of Ranks</returns>
        [HttpGet]
        [Route("ranks")]
        [ResponseType(typeof(List<Rank_QRM>))]
        public IHttpActionResult ListRanks()
        {
            var returnData = _peopleService.ListRanks(Request.GetUserContext().AccessToken);

            return Ok(returnData);
        }

    }
}
