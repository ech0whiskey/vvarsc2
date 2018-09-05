using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.ResponseModels.Organizations;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IOrganizationQueryService
    {
        List<Organization> ListOrganizations(string accessToken);
        Organization GetOrganizationByID(string accessToken, int organizationID);
        List<OrgRole> ListOrgRoles(string accessToken);
        OrgRole GetOrgRoleByID(string accessToken, int roleID);
        List<ListRanks_QRM> ListRanks(string accessToken);
        List<PayGrade> ListPayGrades(string accessToken);
    }
}
