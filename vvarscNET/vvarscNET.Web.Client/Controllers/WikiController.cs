using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;

namespace vvarscNET.Web.Client.Controllers
{
    public class WikiController : Controller
    {
        static readonly IPeopleRestClient RestClient = new PeopleRestClient();

        // GET: Wiki
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ranks()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");
            return View(RestClient.ListRanks(HttpContext));
        }
    }
}