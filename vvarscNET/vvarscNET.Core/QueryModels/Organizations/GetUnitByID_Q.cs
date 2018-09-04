using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.QueryModels.Organizations
{
    public class GetUnitByID_Q : IQuery<Unit>
    {
        public int ID;
    }
}
