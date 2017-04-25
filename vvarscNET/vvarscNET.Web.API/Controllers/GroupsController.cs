using vvarscNET.Core;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using System.Web.Http;
using System.Web.Http.Description;
using vvarscNET.Model.Result;
using vvarscNET.Model.ResponseModels.Groups;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Groups
    /// </summary>
    public class GroupsController : ApiController
    {
        private IGroupsQueryService _groupsQueryService;

        /// <summary>
        /// Constructor for GroupsController
        /// </summary>
        /// <param name="groupsQueryService"></param>
        public GroupsController(IGroupsQueryService groupsQueryService)
        {
            _groupsQueryService = groupsQueryService;
        }

        /// <summary>
        /// Method to Return List of Groups of a Certain Type within a Given Shell
        /// </summary>
        /// <returns>List of Groups</returns>
        /// <param name="groupType">Type of Group to Retrieve</param>
        /// <param name="shellID">ID of the Shell for which to Retrive Groups</param>
        [HttpGet]
        [Route("groups/{shellID}/{groupType}")]
        [ResponseType(typeof(ListGroupsByShellAndGroupType_QRM))]
        public IHttpActionResult ListGroupsByShellAndGroupType([FromUri] int shellID, [FromUri] string groupType)
        {
            var result = _groupsQueryService.ListGroupsByShellAndGroupType(Request.GetUserContext().AccessTokenId, shellID, groupType);

            return Ok(result);
        }
    }
}