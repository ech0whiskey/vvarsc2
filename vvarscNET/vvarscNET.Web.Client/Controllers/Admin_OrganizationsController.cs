using System;
using System.Web.Mvc;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;

namespace vvarscNET.Web.Client.Controllers
{
    public class Admin_OrganizationsController : Controller
    {
        static readonly IOrganizationsRestClient RestClient = new OrganizationsRestClient();

        public ActionResult Index()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsSuperAdmin())
                return RedirectToAction("Forbidden", "Home");

            return View(RestClient.ListOrganizations(HttpContext));
        }
    }
}
