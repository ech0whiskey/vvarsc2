using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Model.ResponseModels.Organizations
{
    public class GetFullOrganization_QRM
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
        public List<Member> Members;
    }
}
