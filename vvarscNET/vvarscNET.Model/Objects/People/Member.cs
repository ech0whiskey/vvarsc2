using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects
{
    public class Member
    {
        public int ID;
        public string UserName;
        public string RSIHandle;
        public int OrganizationID;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
