using System;
using System.Web.Mvc;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;
using vvarscNET.Model.RequestModels.Authentication;

namespace vvarscNET.Web.Client.Controllers
{
    public class OrganizationsController : Controller
    {
        static readonly IOrganizationsRestClient RestClient = new OrganizationsRestClient();

        public ActionResult Index()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Logout", "Authentication");

            return View(RestClient.ListOrganizations(HttpContext));
        }
    }
}
