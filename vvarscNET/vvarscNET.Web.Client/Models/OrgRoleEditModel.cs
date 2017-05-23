using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Web.Client.Models
{
    public class OrgRoleEditModel
    {
        public int ID { get; set; }

        public int OrganizationID { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string RoleShortName { get; set; }

        public string RoleDisplayName { get; set; }

        [Required]
        public string RoleType { get; set; }

        public int OrderBy { get; set; }

        public bool IsActive { get; set; }

        public bool IsHidden { get; set; }

        [Required]
        public List<int> SupportedPayGrades { get; set; }

        //Select Lists
        public List<SelectListItem> RoleTypes { get; set; }
        public List<SelectListItem> PayGrades { get; set; }
    }
}