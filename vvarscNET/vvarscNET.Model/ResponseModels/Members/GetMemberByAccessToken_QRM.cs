using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Members
{
    public class GetMemberByAccessToken_QRM
    {
        public string MemberPID;
        public string Login;
        public string Password;
        public string FirstName;
        public string LastName;
        public string Language;
        public string Region;
        public string TimeZone;
        public int ShellID;
        public int Status;
        public int MemberType;
        public string Customer_ID;
    }
}
