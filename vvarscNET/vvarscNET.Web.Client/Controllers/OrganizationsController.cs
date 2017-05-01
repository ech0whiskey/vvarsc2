using System;
using System.Web.Mvc;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;

namespace vvarscNET.Web.Client.Controllers
{
    public class OrganizationsController : Controller
    {
        static readonly IOrganizationsRestClient RestClient = new OrganizationsRestClient();

        public ActionResult Index()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            return RedirectToAction("Details", "Organizations", new { ID = HttpContext.Session["OrganizationID"] });
        }

        public ActionResult Details(int ID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            return View(RestClient.GetOrganizationByID(HttpContext, ID));
        }
    }
}
