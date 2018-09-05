using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class UpdateRanksForOrgRole_C : ICommand
    {
        public int OrgRoleID;
        public List<int> SupportedRanks;
    }
}
