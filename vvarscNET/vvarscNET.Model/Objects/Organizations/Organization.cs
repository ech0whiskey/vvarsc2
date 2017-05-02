using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.Organizations
{
    public class Organization
    {
        public int ID;
        public string OrganizationName;
        public string OrganizationSpectrumID;
        public string OrganizationWebsiteURL;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
