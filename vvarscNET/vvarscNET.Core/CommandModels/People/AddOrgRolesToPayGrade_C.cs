using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.CommandModels.People
{
    public class AddOrgRolesToPayGrade_C
    {
        public int PayGradeID;
        public List<String> SupportedOrgRoles;
        public bool IsActive;
    }
}
