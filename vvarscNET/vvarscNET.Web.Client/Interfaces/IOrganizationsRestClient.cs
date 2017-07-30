using System.Collections.Generic;
using vvarscNET.Model.Objects.Organizations;
using System.Web;
using vvarscNET.Model.Result;

namespace vvarscNET.Web.Client.Interfaces
{
    public interface IOrganizationsRestClient
    {
        IEnumerable<Organization> ListOrganizations(HttpContextBase Context);

        Organization GetOrganizationByID(HttpContextBase Context, int OrganizationID);

        List<OrgRole> ListRolesForOrganization(HttpContextBase Context, int OrganizationID);

        OrgRole GetOrgRoleByID(HttpContextBase Context, int RoleID);

        Result EditOrgRole(HttpContextBase Context, OrgRole Role);
    }
}
