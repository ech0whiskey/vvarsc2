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
            if (HttpContext.Session["UserType"].ToString() == "SuperAdmin" ||
                HttpContext.Session["UserType"].ToString() == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
                return RedirectToAction("Details", "Organizations", new { id = 1 });
        }
    }
}
