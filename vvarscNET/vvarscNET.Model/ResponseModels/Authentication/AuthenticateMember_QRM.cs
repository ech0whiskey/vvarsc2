using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Authentication
{
    public class AuthenticateMember_QRM
    {
        public int ID;
        public string UserName;
        public string RSIHandle;
        public int OrganizationID;
        public bool IsActive;
    }
}
