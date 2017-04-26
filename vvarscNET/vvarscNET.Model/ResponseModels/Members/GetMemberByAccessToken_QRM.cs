using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Enums;

namespace vvarscNET.Model.ResponseModels.Members
{
    public class GetMemberByAccessToken_QRM
    {
        public int ID;
        public string UserName;
        public string RSIHandle;
        public int OrganizationID;
        public UserTypeEnum UserType;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
