using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Authentication
{
    public class GetAccessToken_QRM
    {
        public Guid AccessTokenID;
        public string MemberPID;
        public int ShellID;
        public int AppID;
        public string ApplicationPID;
        public DateTime ExpiryDate;
        public DateTime OfflineExpiryDate;
        public string TokenType;
        public Guid ParentToken;
        public string PrivateKey;
        public string Version;
        public string Language;
        public string Region;
        public string Timezone;
    }
}
