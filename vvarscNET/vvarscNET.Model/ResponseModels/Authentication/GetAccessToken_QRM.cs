using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Authentication
{
    public class GetAccessToken_QRM
    {
        public int ID;
        public int MemberID;
        public int OrganizationID;
        public string AccessToken;
        public string ParentAccessToken;
        public DateTime ValidFrom;
        public DateTime ValidTo;
    }
}
