using System;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller Repsponsible for Managing People and Related Objects
    /// </summary>
    public class PeopleController : ApiController
    {
        private IPeopleQueryService _peopleQuerySvc;
        private IPeopleCommandService _peopleCommandSvc;

        /// <summary>
        /// Constructor for People Controller
        /// </summary>
        /// <param name="peopleQuerySvc"></param>
        /// <param name="peopleCommandSvc"></param>
        public PeopleController(IPeopleQueryService peopleQuerySvc, IPeopleCommandService peopleCommandSvc)
        {
            _peopleQuerySvc = peopleQuerySvc;
            _peopleCommandSvc = peopleCommandSvc;
        }

        /// <summary>
        /// Method to List all Ranks
        /// </summary>
        /// <returns>List of Ranks</returns>
        [HttpGet]
        [Route("ranks")]
        [ResponseType(typeof(List<ListRanks_QRM>))]
        public IHttpActionResult ListRanks()
        {
            var returnData = _peopleQuerySvc.ListRanks(Request.GetUserContext().AccessToken);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to list Members belonging to an Organization
        /// </summary>
        /// <param name="id">ID of Organization</param>
        /// <returns>List of Members</returns>
        [HttpGet]
        [Route("organizations/{id}/members")]
        [ResponseType(typeof(List<Member>))]
        public IHttpActionResult ListMembersForOrganization([FromUri] int id)
        {
            var returnData = _peopleQuerySvc.ListMembersForOrganization(Request.GetUserContext().AccessToken, id);

            return Ok(returnData);
        }

        /// <summary>
        /// Method to Create a New Member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("members")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult CreateMember([FromBody] Member member)
        {
            var result = _peopleCommandSvc.CreateMember(Request.GetUserContext(), member);

            return Ok(result);
        }

    }
}
