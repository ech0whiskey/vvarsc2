using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Members
{
    public class GetMemberByAccessToken_QRM
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
