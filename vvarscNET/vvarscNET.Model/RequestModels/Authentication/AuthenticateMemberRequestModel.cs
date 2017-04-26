using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.RequestModels.Authentication
{
    public class AuthenticateMemberRequestModel
    {
        [Required]
        public string UserName;
        [Required]
        public string Password;
    }
}
