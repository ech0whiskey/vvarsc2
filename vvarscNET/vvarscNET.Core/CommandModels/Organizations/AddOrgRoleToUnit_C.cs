using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class AddOrgRoleToUnit_C : ICommand
    {
        public int UnitID;
        public int OrgRoleID;
        public bool IsUnitLeadership;
        public string RatingCodeOverride;
        public bool IsActive;
    }
}
