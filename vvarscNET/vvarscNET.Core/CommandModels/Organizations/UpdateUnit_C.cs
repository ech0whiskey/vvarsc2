using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Enums;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class UpdateUnit_C : ICommand
    {
        public int UnitID;
        public int ParentUnitID;
        public string UnitName;
        public string UnitFullName;
        public string UnitDesignation;
        public string UnitDescription;
        public string UnitCallsign;
        public string UnitType;
        public bool IsHidden;
        public bool IsActive;
    }
}
