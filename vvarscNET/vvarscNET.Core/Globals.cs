using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core
{
    public static class Globals
    {
        public static string CoreDBName = "scNET_Core";
        public static readonly string AuthHandlerToken = "00055U8J7bJ4MkWv7x6lMi38Yg";
        public static readonly string ApplicationPID = "AdminCon";
        public static readonly UserContext UserContext = new UserContext
        {
            AccessToken = AuthHandlerToken,
            MemberID = 0,
            OrganizationID = 0
        };
    }
}
