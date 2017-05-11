﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.People
{
    public class CreateOrgRole_C : ICommand
    {
        public string RoleName;
        public string RoleShortName;
        public string RoleDisplayName;
        public string RoleType;
        public int RoleOrderBy;
        public bool IsActive;
        public bool IsHidden;
    }
}
