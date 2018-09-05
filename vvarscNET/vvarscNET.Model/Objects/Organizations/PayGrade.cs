using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Objects.Organizations
{
    public class PayGrade
    {
        public int ID;
        public string PayGradeName;
        public string PayGradeDisplayName;
        public int PayGradeOrderBy;
        public string PayGradeGroup;
        public string PayGradeDescriptionText;
        public string PayGradeNotes;
        public bool IsActive;
        public DateTime CreatedOn;
        public string CreatedBy;
        public DateTime ModifiedOn;
        public string ModifiedBy;
    }
}
