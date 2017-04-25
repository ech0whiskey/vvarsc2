using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Authentication
{
    public class AuthenticateMember_QRM
    {
        public string InstanceID;
        public string MemberPID;
        public int MemberID;
        public string UserName;
        public string Email;
        public string FirstName;
        public string LastName;
        public DateTime LastLoginDate;
        public string Language;
        public string Region;
        public string TimeZone;
        public string Customer_ID;
        public int ShellID;
        public DateTime CreatedOn;
    }
}
