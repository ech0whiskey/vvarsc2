using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Authentication
{
    public class CreateAccessToken_C : ICommand
    {
        public int MemberID;
        public int? OrganizationID;
        public DateTime ValidFrom;
        public DateTime ValidTo;
        public string ParentAccessToken;
    }
}
