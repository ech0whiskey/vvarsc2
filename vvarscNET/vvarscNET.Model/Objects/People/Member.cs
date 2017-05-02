using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.People
{
    public class Member
    {
        public int ID;
        public string UserName;
        public string RSIHandle;
        public int OrganizationID;
        public string UserType;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
