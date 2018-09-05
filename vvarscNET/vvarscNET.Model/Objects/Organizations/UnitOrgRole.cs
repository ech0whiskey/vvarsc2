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
        public string RoleName;
        public string RoleShortName;
        public string RoleDisplayName;
        public string RoleType;
        public int RoleOrderBy;
        public string RatingCodeOverride;
        public bool IsUnitLeadership;
        public bool IsActive;
        public bool IsHidden;
        public List<Rank> SupportedRanks;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
