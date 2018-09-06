﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IUnitCommandService
    {
        Result CreateUnit(UserContext context, Unit unit);
        Result UpdateUnit(UserContext context, Unit unit);
        Result DeleteUnit(UserContext context, int unitID);
        Result DeleteUnitRecursive(UserContext context, int unitID);
        Result AddOrgRoleToUnit(UserContext context, int unitID, UnitOrgRole orgRole);
        Result RemoveOrgRoleFromUnit(UserContext context, int unitID, int orgRoleID);
    }
}