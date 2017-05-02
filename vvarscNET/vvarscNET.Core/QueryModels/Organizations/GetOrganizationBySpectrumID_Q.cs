using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.QueryModels.Organizations
{
    public class GetOrganizationBySpectrumID_Q : IQuery<Organization>
    {
        public string SpectrumID;
    }
}
