using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class RemoveOrgRoleFromUnit_C : ICommand
    {
        public int UnitID;
        public int OrgRoleID;
    }
}
