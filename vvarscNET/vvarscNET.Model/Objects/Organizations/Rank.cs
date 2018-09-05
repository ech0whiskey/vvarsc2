using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.Organizations
{
    public class Rank
    {
        public int ID;
        public int PayGradeID;
        public string RankName;
        public string RankAbbr;
        public string RankType;
        public string RankImage;
        public string RankGroupName;
        public string RankGroupImage;
        public string RatingCodeSuffix;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
