using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace vvarscNET.Web.Client.Models
{
    public class MemberEditModel
    {
        //Fields
        [Display(Name = "Member ID")]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string RSIHandle { get; set; }

        public int OrganizationID { get; set; }

        [Required]
        public string UserType { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Member Rank")]
        [Required]
        public int RankID { get; set; }

        //Select Lists
        public List<SelectListItem> Ranks { get; set; }
        public List<SelectListItem> UserTypes { get; set; }
    }
}