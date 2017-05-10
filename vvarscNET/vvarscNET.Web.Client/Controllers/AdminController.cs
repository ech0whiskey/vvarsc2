using System;
using System.Web.Mvc;
using System.Collections.Generic;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;
using vvarscNET.Web.Client.Models;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Web.Client.Controllers
{
    public class AdminController : Controller
    {
        static readonly IOrganizationsRestClient orgRestClient = new OrganizationsRestClient();
        static readonly IPeopleRestClient peopleRestClient = new PeopleRestClient();

        public ActionResult Index()
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            //return View(RestClient.ListOrganizations(HttpContext));
            return View();
        }

        public ActionResult Organization(int ID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            return View(orgRestClient.GetOrganizationByID(HttpContext, ID));
        }

        public ActionResult OrganizationMembers(int ID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            return View(peopleRestClient.ListMembersForOrganization(HttpContext, ID));
        }

        public ActionResult CreateMember(int ID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            //RANKS SELECT LIST
            var ranks = peopleRestClient.ListRanks(HttpContext);
            var model = new MemberEditModel
            {
                OrganizationID = ID,
                Ranks = new List<SelectListItem>(),
                UserTypes = new List<SelectListItem>()
            };

            //Populate Default for SelectList
            model.Ranks.Add(new SelectListItem
            {
                Value = null,
                Text = "-- Select a Rank --",
                Selected = true
            });

            foreach (var rank in ranks)
            {
                model.Ranks.Add(new SelectListItem
                {
                    Value = rank.RankID.ToString(),
                    Text = "[" + rank.PayGradeDisplayName + "] " + rank.RankName
                });
            }

            //USERTYPE SELECT LIST
            model.UserTypes.Add(new SelectListItem
            {
                Value = null,
                Text = "-- Select a Type --",
                Selected = true
            });
            model.UserTypes.Add(new SelectListItem
            {
                Value = "Admin",
                Text = "Admin"
            });
            model.UserTypes.Add(new SelectListItem
            {
                Value = "Officer",
                Text = "Officer"
            });
            model.UserTypes.Add(new SelectListItem
            {
                Value = "Member",
                Text = "Member"
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMember(int? organizationID, MemberEditModel member)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            try
            {
                var memToCreate = new Member
                {
                    UserName = member.UserName,
                    RSIHandle = member.RSIHandle,
                    OrganizationID = organizationID ?? default(int),
                    RankID = member.RankID,
                    UserType = member.UserType,
                    IsActive = member.IsActive
                };

                var result = peopleRestClient.CreateMember(HttpContext, memToCreate);

                return RedirectToAction("OrganizationMembers", "Admin", new { id = organizationID });
            }
            catch
            {
                return View();
            }
        }
    }
}
