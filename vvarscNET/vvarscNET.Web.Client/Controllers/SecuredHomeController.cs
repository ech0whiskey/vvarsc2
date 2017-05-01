using System;
using System.Web.Mvc;
using vvarscNET.Web.Client.Helper;
using vvarscNET.Model.RequestModels.Authentication;

namespace vvarscNET.Web.Client.Controllers
{
    public class SecuredHomeController : Controller
    {
        public ActionResult Index()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Logout", "Authentication");

            //return View();
            return RedirectToAction("Details", "Organizations", new { ID = HttpContext.Session["OrganizationID"] });
        }
    }
}
