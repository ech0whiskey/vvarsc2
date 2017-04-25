using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.RequestModels.Authentication
{
    public class LogoutMemberRequestModel
    {
        [Required]
        public string MemberPID;
        [Required]
        public int ShellID;
        [Required]
        public string AccessTokenID;
    }
}
