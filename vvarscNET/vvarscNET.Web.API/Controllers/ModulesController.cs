using vvarscNET.Core;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using System.Web.Http;
using System.Web.Http.Description;
using vvarscNET.Model.Result;
using vvarscNET.Model.ResponseModels.Modules;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Modules
    /// </summary>
    public class ModulesController : ApiController
    {
        private IModulesQueryService _modulesQueryService;

        /// <summary>
        /// Constructor for ModulesController
        /// </summary>
        /// <param name="modulesQueryService"></param>
        public ModulesController(IModulesQueryService modulesQueryService)
        {
            _modulesQueryService = modulesQueryService;
        }

        /// <summary>
        /// Method to Return List of Feed (READ + PING) Modules for a Given Account (Shell)
        /// </summary>
        /// <returns>List of Feed Modules</returns>
        /// <param name="shellID">ID of Account (Shell) for which to retrive Feed Modules</param>
        [HttpGet]
        [Route("modules/{shellID}/feeds")]
        [ResponseType(typeof(ListFeedModulesForShell_QRM))]
        public IHttpActionResult ListFeedModulesForShell([FromUri] int shellID)
        {
            var result = _modulesQueryService.ListFeedModulesForShell(Request.GetUserContext().AccessTokenId, shellID);

            return Ok(result);
        }

        /// <summary>
        /// Method to Return List of Library Modules for a Given Account (Shell)
        /// </summary>
        /// <returns>List of Library Modules</returns>
        /// <param name="shellID">ID of Account (Shell) for which to retrive Library Modules</param>
        [HttpGet]
        [Route("modules/{shellID}/libraries")]
        [ResponseType(typeof(ListLibraryModulesForShell_QRM))]
        public IHttpActionResult ListLibraryModulesForShell([FromUri] int shellID)
        {
            var result = _modulesQueryService.ListLibraryModulesForShell(Request.GetUserContext().AccessTokenId, shellID);

            return Ok(result);
        }
    }
}