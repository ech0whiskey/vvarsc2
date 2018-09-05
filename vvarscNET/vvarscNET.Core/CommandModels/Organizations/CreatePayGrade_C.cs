using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class CreatePayGrade_C : ICommand
    {
        public string PayGradeName;
        public string PayGradeDisplayName;
        public int PayGradeOrderBy;
        public string PayGradeGroup;
        public string PayGradeDescriptionText;
        public string PayGradeNotes;
        public bool IsActive;
    }
}
