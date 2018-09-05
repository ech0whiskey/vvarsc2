using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Model.Objects.Organizations
{
    public class UnitOrgRole
    {
        public int ID;
        public int UnitID;
        public int OrgRoleID;
        public int RoleOrderBy;
        public bool IsUnitLeadership;
        public bool IsActive;
        public bool IsHidden;
        public List<PayGrade> SupportedPayGrades;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
