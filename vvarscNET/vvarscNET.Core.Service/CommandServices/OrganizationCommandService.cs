using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.CommandModels.Organizations;
using vvarscNET.Model.Result;
using vvarscNET.Model.Enums;
using System.Security.Cryptography;
using vvarscNET.Core.CommandModels.Authentication;

namespace vvarscNET.Core.Service.CommandServices
{
    public class OrganizationCommandService : IOrganizationCommandService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public OrganizationCommandService(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }
        
        public Result UpdateOrgRole(UserContext context, OrgRole role)
        {
            //Get Existing OrgRole
            var roleQuery = new GetOrgRoleByID_Q
            {
                ID = role.ID
            };

            var existingRole = _queryDispatcher.Dispatch<GetOrgRoleByID_Q, OrgRole>(context.AccessToken, roleQuery);
            if (existingRole == null)
                throw new Exception("Unable to retrive Role for Update Request");

            //Update OrgRole
            var roleCmd = new UpdateOrgRole_C
            {
                ID = role.ID,
                OrganizationID = role.OrganizationID,
                RoleName = role.RoleName,
                RoleShortName = role.RoleShortName,
                RoleDisplayName = role.RoleDisplayName,
                RoleType = role.RoleType,
                RoleOrderBy = role.RoleOrderBy,
                RatingCode = role.RatingCode,
                IsActive = role.IsActive,
                IsHidden = role.IsHidden
            };

            var roleResult = _commandDispatcher.Dispatch<UpdateOrgRole_C>(context, roleCmd);
            if (roleResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error updating Organization Role in DB");
            }

            //Update Mapped Ranks
            var rCmd = new UpdateRanksForOrgRole_C
            {
                OrgRoleID = role.ID,
                SupportedRanks = new List<int>()
            };
            foreach (var r in role.SupportedRanks)
            {
                rCmd.SupportedRanks.Add(r.ID);
            }

            var pgResult = _commandDispatcher.Dispatch<UpdateRanksForOrgRole_C>(context, rCmd);
            if (pgResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error updating Organization Role - Rank Mapping in DB");
            }

            return roleResult;
        }
    }
}
