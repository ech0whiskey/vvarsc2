using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.RequestModels.Authentication
{
    public class RenewTokenRequestModel
    {
        [Required]
        public int MemberID;
        [Required]
        public int OrganizationID;
        [Required]
        public string AccessToken;
    }
}
