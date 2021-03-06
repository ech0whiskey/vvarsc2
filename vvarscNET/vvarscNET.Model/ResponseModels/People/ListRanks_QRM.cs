﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Model.ResponseModels.People
{
    public class ListRanks_QRM
    {
        public int RankID;
        public int PayGradeID;
        public string PayGradeName;
        public string PayGradeDisplayName;
        public int PayGradeOrderBy;
        public string PayGradeGroup;
        public string PayGradeDescriptionText;
        public string PayGradeNotes;
        public string RankName;
        public string RankAbbr;
        public string RankType;
        public string RankImage;
        public string RankGroupName;
        public string RankGroupImage;
        public List<OrgRole> SupportedOrgRoles;
    }
}
