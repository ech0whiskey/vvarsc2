using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.People
{
    public class CreateRank_C : ICommand
    {
        public int PayGradeID;
        public string RankName;
        public string RankAbbr;
        public string RankType;
        public string RankImage;
        public string RankGroupName;
        public string RankGroupImage;
        public string RatingCodeSuffix;
        public bool IsActive;
    }
}
