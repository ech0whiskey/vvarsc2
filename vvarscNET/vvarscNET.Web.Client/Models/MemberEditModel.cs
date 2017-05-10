using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Web.Client.Models
{
    public class MemberEditModel
    {
        //Fields
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