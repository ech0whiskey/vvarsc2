using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IOrganizationCommandService
    {
        Result UpdateOrgRole(UserContext context, OrgRole role);
    }
}
