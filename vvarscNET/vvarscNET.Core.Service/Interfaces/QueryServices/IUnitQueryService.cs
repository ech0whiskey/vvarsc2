using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.ResponseModels.Organizations;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IUnitQueryService
    {
        List<Unit> ListUnits(string accessToken);
        Unit GetUnitByID(string accessToken, int UnitID, bool includeChildren);
        List<UnitOrgRole> ListOrgRolesForUnit(string accessToken, int unitID);
    }
}
