using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.ResponseModels.Organizations;

namespace vvarscNET.Core.Service.QueryServices
{
    public class UnitQueryService : IUnitQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public UnitQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<Unit> ListUnits(string accessToken)
        {
            var query = new ListUnits_Q();

            var result = _queryDispatcher.Dispatch<ListUnits_Q, List<Unit>>(accessToken, query);

            return result;
        }

        public Unit GetUnitByID(string accessToken, int unitID, bool includeChildren)
        {
            var query = new GetUnitByID_Q
            {
                ID = unitID,
                IncludeChildren = includeChildren
            };

            var result = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(accessToken, query);

            return result;
        }

        public List<UnitOrgRole> ListOrgRolesForUnit(string accessToken, int unitID)
        {
            var query = new ListOrgRolesForUnit_Q
            {
                UnitID = unitID
            };

            var result = _queryDispatcher.Dispatch<ListOrgRolesForUnit_Q, List<UnitOrgRole>>(accessToken, query);

            return result;
        }
    }
}
