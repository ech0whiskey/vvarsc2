﻿using System.Web.Mvc;

namespace vvarscNET.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This is a demo app showing how to consume asp.net web api using RestSharp.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About demo app.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }
    }
}
