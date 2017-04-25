using vvarscNET.Core;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Model.RequestModels.Members;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Model.Result;
using System.Web.Http;
using System.Web.Http.Description;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Member object
    /// </summary>
    public class MembersController : ApiController
    {
        private IMembersQueryService _memberQueryService;
        private IMemberCommandService _memberCommandService;

        /// <summary>
        /// Constructor for Members Controller
        /// </summary>
        /// <param name="memberQueryService"></param>
        /// <param name="memberCommandService"></param>
        public MembersController(IMembersQueryService memberQueryService, IMemberCommandService memberCommandService)
        {
            _memberQueryService = memberQueryService;
            _memberCommandService = memberCommandService;
        }

        /// <summary>
        /// Method to GET a Member by MemberPID
        /// </summary>
        /// <param name="memberPID">Unique ID of Member to GET</param>
        /// <returns>Member</returns>
        [HttpGet]
        [Route("members/{memberPID}")]
        [ResponseType(typeof(GetMemberByPID_QRM))]
        public IHttpActionResult GetMemberByPID([FromUri] string memberPID)
        {
            var result = _memberQueryService.GetMemberByPID(Request.GetUserContext().AccessTokenId, memberPID);

            return Ok(result);
        }

        /// <summary>
        /// Method to Update a Member
        /// </summary>
        /// <param name="member">Member Object to Update</param>
        /// <returns>Result Object</returns>
        [HttpPut]
        [Route("members/")]
        [ResponseType(typeof(Result))]
        public IHttpActionResult UpdateMember([FromBody] UpdateMemberRequestModel member)
        {
            var result = _memberCommandService.UpdateMember(Request.GetUserContext(), member);

            return Ok(result);
        }
    }
}