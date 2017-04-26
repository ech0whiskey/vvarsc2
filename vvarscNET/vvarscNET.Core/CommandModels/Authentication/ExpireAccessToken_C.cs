using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Authentication
{
    public class ExpireAccessToken_C : ICommand
    {
        public string AccessToken;
        public int OrganizationID;
        public int MemberID;
    }
}
