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
        public string MemberPID;
        public int ShellID;
        public int AppID;
        public string ApplicationPID;
        public DateTime ExpiryDate;
        public DateTime OfflineExpiryDate;
        public string TokenType;
        public string ParentToken;
        public string Version;
    }
}
