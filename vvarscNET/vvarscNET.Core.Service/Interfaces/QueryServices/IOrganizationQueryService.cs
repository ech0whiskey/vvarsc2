using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IOrganizationQueryService
    {
        List<Organization> ListOrganizations(string accessToken);
        Organization GetOrganizationByID(string accessToken, int organizationID);
        List<OrgRole> ListRoles(string accessToken);
        OrgRole GetOrgRoleByID(string accessToken, int roleID);
        List<Unit> ListUnits(string accessToken);
        Unit GetUnitByID(string accessToken, int UnitID, bool includeChildren);
    }
}
