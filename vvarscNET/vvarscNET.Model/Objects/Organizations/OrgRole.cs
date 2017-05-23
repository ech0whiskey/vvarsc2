using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Model.Objects.Organizations
{
    public class OrgRole
    {
        public int ID;
        public int OrganizationID;
        public string RoleName;
        public string RoleShortName;
        public string RoleDisplayName;
        public string RoleType;
        public int RoleOrderBy;
        public bool IsActive;
        public bool IsHidden;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
        public List<PayGrade> SupportedPayGrades;
    }
}
