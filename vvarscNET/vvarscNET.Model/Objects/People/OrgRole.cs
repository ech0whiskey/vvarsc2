using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.People
{
    public class OrgRole
    {
        public int ID;
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
    }
}
