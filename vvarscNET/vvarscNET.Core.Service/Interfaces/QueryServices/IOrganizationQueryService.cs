﻿using System;
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
        List<OrgRole> ListRolesForOrganization(string accessToken, int organizationID);
        OrgRole GetOrgRoleByID(string accessToken, int roleID);
    }
}
