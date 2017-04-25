using vvarscNET.Core;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Core.Service.Interfaces.QueryServices;
using System.Web.Http;
using System.Web.Http.Description;
using vvarscNET.Model.Result;
using vvarscNET.Model.ResponseModels.Accounts;

namespace vvarscNET.Web.API.Controllers
{
    /// <summary>
    /// Controller for Accounts (Shells)
    /// </summary>
    public class AccountsController : ApiController
    {
        private IAccountsQueryService _accountsQueryService;

        /// <summary>
        /// Constructor for AccountsController
        /// </summary>
        /// <param name="accountsQueryService"></param>
        public AccountsController(IAccountsQueryService accountsQueryService)
        {
            _accountsQueryService = accountsQueryService;
        }

        /// <summary>
        /// Method to Return List of Active Accounts (Shells)
        /// </summary>
        /// <returns>List of Shells</returns>
        [HttpGet]
        [Route("accounts/")]
        [ResponseType(typeof(ListShells_QRM))]
        public IHttpActionResult ListActiveShells()
        {
            var result = _accountsQueryService.ListActiveShells(Request.GetUserContext().AccessTokenId);

            return Ok(result);
        }
    }
}