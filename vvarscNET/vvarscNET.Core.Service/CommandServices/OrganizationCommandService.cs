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

        //Roles
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

            //Update Mapped PayGrades
            var pgCmd = new UpdatePayGradesForOrgRole_C
            {
                OrgRoleID = role.ID,
                SupportedPayGrades = new List<int>()
            };
            foreach (var pg in role.SupportedPayGrades)
            {
                pgCmd.SupportedPayGrades.Add(pg.ID);
            }

            var pgResult = _commandDispatcher.Dispatch<UpdatePayGradesForOrgRole_C>(context, pgCmd);
            if (pgResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error updating Organization Role - PayGrade Mapping in DB");
            }

            return roleResult;
        }

        //Units
        public Result CreateUnit(UserContext context, Unit unit)
        {
            var cmd = new CreateUnit_C
            {
                ParentUnitID = unit.ParentUnitID,
                ParentUnitName = unit.ParentUnitName,
                UnitName = unit.UnitName,
                UnitFullName = unit.UnitFullName,
                UnitDesignation = unit.UnitDesignation,
                UnitDescription = unit.UnitDescription,
                UnitCallsign = unit.UnitCallsign,
                UnitType = unit.UnitType.ToString(),
                IsHidden = unit.IsHidden,
                IsActive = unit.IsActive
            };

            var result = _commandDispatcher.Dispatch<CreateUnit_C>(context, cmd);
            if (result.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error Creating Unit in DB");
            }

            return result;
        }

        public Result UpdateUnit(UserContext context, Unit unit)
        {
            //get existing unit
            var query = new GetUnitByID_Q
            {
                ID = unit.ID
            };

            var existingUnit = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(context.AccessToken, query);
            if (existingUnit == null)
                throw new Exception("Unable to retrive Unit for Update Request");

            var cmd = new UpdateUnit_C
            {
                ParentUnitID = unit.ParentUnitID,
                UnitName = unit.UnitName,
                UnitFullName = unit.UnitFullName,
                UnitDesignation = unit.UnitDesignation,
                UnitDescription = unit.UnitDescription,
                UnitCallsign = unit.UnitCallsign,
                UnitType = unit.UnitType.ToString(),
                IsHidden = unit.IsHidden,
                IsActive = unit.IsActive
            };

            var unitResult = _commandDispatcher.Dispatch<UpdateUnit_C>(context, cmd);
            if (unitResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error updating Unit in DB");
            }

            return unitResult;
        }

        public Result DeleteUnit(UserContext context, int unitID)
        {
            //get existing unit
            var query = new GetUnitByID_Q
            {
                ID = unitID
            };

            var existingUnit = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(context.AccessToken, query);
            if (existingUnit == null)
                throw new Exception("Unable to retrive Unit for Delete Request");

            //delete unit
            var cmd = new DeleteUnit_C
            {
                UnitID = unitID
            };

            var result = _commandDispatcher.Dispatch<DeleteUnit_C>(context, cmd);
            if (result.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error Deleting Unit in DB");
            }

            return result;
        }

        public Result DeleteUnitRecursive(UserContext context, int unitID)
        {
            //get existing unit
            var query = new GetUnitByID_Q
            {
                ID = unitID
            };

            var existingUnit = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(context.AccessToken, query);
            if (existingUnit == null)
                throw new Exception("Unable to retrive Unit for Delete Request");

            //delete unit
            var cmd = new DeleteUnitRecursive_C
            {
                UnitID = unitID
            };

            var result = _commandDispatcher.Dispatch<DeleteUnitRecursive_C>(context, cmd);
            if (result.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error Deleting Unit in DB");
            }

            return result;
        }
    }
}
