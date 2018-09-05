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
    public class OrganizationQueryService : IOrganizationQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public OrganizationQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<Organization> ListOrganizations(string accessToken)
        {
            var query = new ListOrganizations_Q();

            var result = _queryDispatcher.Dispatch<ListOrganizations_Q, List<Organization>>(accessToken, query);

            return result;
        }

        public Organization GetOrganizationByID(string accessToken, int organizationID)
        {
            var query = new GetOrganizationByID_Q
            {
                ID = organizationID
            };

            var result = _queryDispatcher.Dispatch<GetOrganizationByID_Q, Organization>(accessToken, query);

            return result;
        }

        public List<OrgRole> ListOrgRoles(string accessToken)
        {
            var query = new ListRoles_Q();

            var result = _queryDispatcher.Dispatch<ListRoles_Q, List<OrgRole>>(accessToken, query);

            return result;
        }

        public OrgRole GetOrgRoleByID(string accessToken, int roleID)
        {
            var query = new GetOrgRoleByID_Q
            {
                ID = roleID
            };

            var result = _queryDispatcher.Dispatch<GetOrgRoleByID_Q, OrgRole>(accessToken, query);

            return result;
        }

        public List<ListRanks_QRM> ListRanks(string accessToken)
        {
            var query = new ListRanks_Q();

            var result = _queryDispatcher.Dispatch<ListRanks_Q, List<ListRanks_QRM>>(accessToken, query);

            return result;
        }

        public List<PayGrade> ListPayGrades(string accessToken)
        {
            var query = new ListPayGrades_Q { };

            var result = _queryDispatcher.Dispatch<ListPayGrades_Q, List<PayGrade>>(accessToken, query);

            return result;
        }

        public List<Unit> ListUnits(string accessToken)
        {
            var query = new ListUnits_Q();

            var result = _queryDispatcher.Dispatch<ListUnits_Q, List<Unit>>(accessToken, query);

            return result;
        }

        public Unit GetUnitByID(string accessToken, int UnitID, bool includeChildren)
        {
            var query = new GetUnitByID_Q
            {
                ID = UnitID,
                IncludeChildren = includeChildren
            };

            var result = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(accessToken, query);

            return result;
        }
    }
}
