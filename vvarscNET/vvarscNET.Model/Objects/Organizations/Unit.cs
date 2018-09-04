using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Enums;

namespace vvarscNET.Model.Objects.Organizations
{
    public class Unit
    {
        public int ID;
        public int ParentUnitID;
        public string ParentUnitName;
        public string UnitName;
        public string UnitFullName;
        public string UnitDesignation;
        public string UnitDescription;
        public string UnitCallsign;
        public UnitTypeEnum UnitType;
        public List<Unit> ChildUnits;
        public bool IsHidden;
        public bool IsActive;
        public DateTime CreatedOn;
        public int CreatedBy;
        public DateTime ModifiedOn;
        public int ModifiedBy;
    }
}
