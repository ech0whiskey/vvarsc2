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
        public string AccessTokenId { get; set; }

        public string MemberPID { get; set; }

        public int ShellID { get; set; }
    }
}
