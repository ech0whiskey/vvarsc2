using System.Web.Mvc;
using vvarscNET.Web.Client.Interfaces;
using vvarscNET.Web.Client.Services;
using vvarscNET.Model.RequestModels.Authentication;
using System;

namespace vvarscNET.Web.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        static readonly IAuthenticationRestClient RestClient = new AuthenticationRestClient();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessLogin(FormCollection collection)
        {
            try
            {
                var model = new AuthenticateMemberRequestModel
                {
                    UserName = collection["UserName"],
                    Password = collection["Password"]
                };
                var result = RestClient.Login(model);
                if (result.AccessTokenValue != null)
                {
                    Session.Add("AccessToken", result.AccessTokenValue);
                    Session.Add("ExpirationDate", result.ValidTo);
                    Session.Add("UserName", model.UserName);
                    Session.Add("MemberID", result.MemberID);
                    Session.Add("OrganizationID", result.OrganizationID);

                    return RedirectToAction("Index", "SecuredHome");
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            try
            {
                var model = new LogoutMemberRequestModel
                {
                    AccessToken = Session["AccessToken"].ToString(),
                    MemberID = Convert.ToInt32(Session["MemberID"]),
                    OrganizationID = Convert.ToInt32(Session["OrganizationID"]),
                };

                var result = RestClient.Logout(model);

                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Logout", "Home");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Logout", "Home");
            }
        }
    }
}
