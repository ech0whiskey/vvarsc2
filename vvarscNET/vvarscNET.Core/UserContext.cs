using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core
{
    public class UserContext : IUserContext
    {
        public string AccessToken { get; set; }

        public int MemberID { get; set; }

        public int OrganizationID { get; set; }
    }
}
