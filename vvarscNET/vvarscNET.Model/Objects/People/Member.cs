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
        public string ModifiedBy;
        public int PayGradeID;
        public string PayGradeName;
        public string PayGradeDisplayName;
        public string PayGradeGroup;
        public int RankID;
        public string RankName;
        public string RankAbbr;
        public string RankType;
        public string RankImage;
        public string RankGroupName;
        public string RankGroupImage;

        //Common Fields
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
    }
}
