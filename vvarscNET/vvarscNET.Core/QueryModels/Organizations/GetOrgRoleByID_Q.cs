using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.QueryModels.Organizations
{
    public class GetOrgRoleByID_Q : IQuery<OrgRole>
    {
        public int ID;
    }
}
