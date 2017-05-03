using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Authentication
{
    public class CreateCredential_C : ICommand
    {
        public int MemberID;
        public string UserName;
        public string PasswordHash;
        public int? OrganizationID;
    }
}
