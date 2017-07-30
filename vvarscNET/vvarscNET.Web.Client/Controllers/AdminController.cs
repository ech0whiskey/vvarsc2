using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Web.Client.Helper;
using vvarscNET.Web.Client.Models;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.Objects.Organizations;

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

            ViewBag.OrganizationID = HttpContext.Session["OrganizationID"];

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

        public ActionResult OrgMembers(int ID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            return View(peopleRestClient.ListMembersForOrganization(HttpContext, ID));
        }

        public ActionResult CreateMember(int organizationID)
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
                OrganizationID = organizationID,
                Ranks = new List<SelectListItem>(),
                UserTypes = new List<SelectListItem>()
            };

            //Populate Default for SelectList
            model.Ranks.Add(new SelectListItem
            {
                Value = "",
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
                Value = "",
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
                
                this.AddToastMessage(result);

                return RedirectToAction("OrgMembers", "Admin", new { id = organizationID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditMember(int memberID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            var member = peopleRestClient.GetMemberByID(HttpContext, memberID);
            if (member == null)
                throw new Exception("Unable to retrive Member for Edit");

            var model = new MemberEditModel
            {
                ID = member.ID,
                UserName = member.UserName,
                RSIHandle = member.RSIHandle,
                OrganizationID = member.OrganizationID,
                UserType = member.UserType,
                IsActive = member.IsActive,
                RankID = member.RankID,
                Ranks = new List<SelectListItem>(),
                UserTypes = new List<SelectListItem>()
            };

            //RANKS SELECT LIST
            var ranks = peopleRestClient.ListRanks(HttpContext);

            //Populate Default for SelectList
            model.Ranks.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Select a Rank --"
            });

            foreach (var rank in ranks)
            {
                model.Ranks.Add(new SelectListItem
                {
                    Value = rank.RankID.ToString(),
                    Text = "[" + rank.PayGradeDisplayName + "] " + rank.RankName
                });
            }

            //Set Current value for RankID as Selected
            model.Ranks.Where(a => a.Value == model.RankID.ToString()).FirstOrDefault().Selected = true;

            //USERTYPE SELECT LIST
            model.UserTypes.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Select a Type --"
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

            model.UserTypes.Where(a => a.Value == model.UserType).FirstOrDefault().Selected = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditMember(MemberEditModel member)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            try
            {
                var memToEdit = new Member
                {
                    ID = member.ID,
                    UserName = member.UserName,
                    RSIHandle = member.RSIHandle,
                    OrganizationID = member.OrganizationID,
                    RankID = member.RankID,
                    UserType = member.UserType,
                    IsActive = member.IsActive
                };
                
                var result = peopleRestClient.EditMember(HttpContext, memToEdit);

                this.AddToastMessage(result);

                return RedirectToAction("EditMember", "Admin", new { memberID = member.ID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteMember(int memberID, int organizationID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            try
            {
                var result = peopleRestClient.DeleteMember(HttpContext, memberID);

                this.AddToastMessage(result);

                return RedirectToAction("OrganizationMembers", "Admin", new { id = organizationID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrgRoles(int organizationID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            return View(orgRestClient.ListRolesForOrganization(HttpContext, organizationID));
        }

        public ActionResult EditOrgRole(int roleID)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            var orgRole = orgRestClient.GetOrgRoleByID(HttpContext, roleID);
            if (orgRole == null)
                throw new Exception("Unable to retrive OrgRole for Edit");

            var model = new OrgRoleEditModel
            {
                ID = orgRole.ID,
                OrganizationID = orgRole.OrganizationID,
                RoleName = orgRole.RoleName,
                RoleShortName = orgRole.RoleShortName,
                RoleDisplayName = orgRole.RoleDisplayName,
                RoleType = orgRole.RoleType,
                OrderBy = orgRole.RoleOrderBy,
                IsActive = orgRole.IsActive,
                IsHidden = orgRole.IsHidden,
                SupportedPayGrades = orgRole.SupportedPayGrades.Select(a => a.ID).Distinct().ToList(),
                RoleTypes = new List<SelectListItem>(),
                PayGrades = new List<SelectListItem>()
            };

            //PayGrades Multi-Select List
            var payGrades = peopleRestClient.ListPayGrades(HttpContext);

            foreach (var pg in payGrades)
            {
                bool selected = false;
                if (model.SupportedPayGrades.Contains(pg.ID))
                    selected = true;

                model.PayGrades.Add(new SelectListItem
                {
                    Value = pg.ID.ToString(),
                    Text = pg.PayGradeName,
                    Selected = selected
                });
            }

            //RoleType SELECT LIST
            model.RoleTypes.Add(new SelectListItem
            {
                Value = "",
                Text = "-- Select a Type --"
            });
            model.RoleTypes.Add(new SelectListItem
            {
                Value = "UnitRole",
                Text = "Unit Role"
            });

            model.RoleTypes.Where(a => a.Value == model.RoleType).FirstOrDefault().Selected = true;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditOrgRole(OrgRoleEditModel role)
        {
            var helper = new HelperFunctions(HttpContext);
            if (!helper.CheckValidSession())
                return RedirectToAction("Unauthorized", "Home");

            if (!helper.IsAdmin())
                return RedirectToAction("Forbidden", "Home");

            try
            {
                var roleToEdit = new OrgRole
                {
                    ID = role.ID,
                    OrganizationID = role.OrganizationID,
                    RoleName = role.RoleName,
                    RoleShortName = role.RoleShortName,
                    RoleDisplayName = role.RoleDisplayName,
                    RoleType = role.RoleType,
                    RoleOrderBy = role.OrderBy,
                    IsActive = role.IsActive,
                    IsHidden = role.IsHidden,
                    SupportedPayGrades = new List<PayGrade>()
                };

                foreach(var pg in role.SupportedPayGrades)
                {
                    roleToEdit.SupportedPayGrades.Add(
                        new PayGrade
                        {
                            ID = pg
                        }    
                    );
                }

                var result = orgRestClient.EditOrgRole(HttpContext, roleToEdit);

                this.AddToastMessage(result);

                return RedirectToAction("EditOrgRole", "Admin", new { roleID = role.ID });
            }
            catch
            {
                return View();
            }

        }
    }
}
