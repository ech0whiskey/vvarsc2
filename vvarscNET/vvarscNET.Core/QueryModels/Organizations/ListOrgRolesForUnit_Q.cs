using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.Organizations;
using System.Collections.Generic;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Core.QueryModels.Organizations
{
    public class ListOrgRolesForUnit_Q : IQuery<List<UnitOrgRole>>
    {
        public int UnitID;
    }
}
