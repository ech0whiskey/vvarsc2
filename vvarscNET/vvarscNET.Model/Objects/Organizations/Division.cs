using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.Organizations
{
    public class Division
    {
        public int ID;
        public int OrganizationID;
        public string DivisionName;
        public bool IsActive;
        public DateTime CreatedOn;
        public int CreatedBy;
        public DateTime ModifiedOn;
        public int ModifiedBy;
    }
}
