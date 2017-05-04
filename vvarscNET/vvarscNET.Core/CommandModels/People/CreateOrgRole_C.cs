using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.CommandModels.People
{
    public class CreateOrgRole_C
    {
        public string RoleName;
        public string RoleShortName;
        public string RoleDisplayName;
        public string RoleType;
        public int RoleOrderBy;
        public bool IsActive;
        public bool IsHidden;
    }
}
