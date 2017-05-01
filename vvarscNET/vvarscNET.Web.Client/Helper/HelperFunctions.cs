using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vvarscNET.Web.Client.Helper
{
    public class HelperFunctions
    {
        private HttpContextBase Context { get; set; }

        public HelperFunctions(HttpContextBase context)
        {
            Context = context;
        }

        public bool CheckValidSession()
        {
            if (Context.Session["AccessToken"] == null ||
               Convert.ToDateTime(Context.Session["ExpirationDate"]) < DateTime.UtcNow)
                return false;
            else
                return true;
        }

        public bool IsSuperAdmin()
        {
            if (Context.Session["UserType"].ToString() == "SuperAdmin")
                return true;
            else
                return false;
        }
    }
}