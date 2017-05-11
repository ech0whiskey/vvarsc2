﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.People
{
    public class AddOrgRolesToPayGrade_C : ICommand
    {
        public int PayGradeID;
        public List<String> SupportedOrgRoles;
        public bool IsActive;
    }
}