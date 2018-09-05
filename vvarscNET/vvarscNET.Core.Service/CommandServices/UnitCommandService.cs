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
    public class UnitCommandService : IUnitCommandService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public UnitCommandService(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

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

        public Result AddOrgRoleToUnit(UserContext context, int unitID, UnitOrgRole orgRole)
        {
            //get existing Unit
            var unitQuery = new GetUnitByID_Q
            {
                ID = unitID
            };

            var existingUnit = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(context.AccessToken, unitQuery);
            if (existingUnit == null)
                throw new Exception("Unable to retrive Unit for Unit-OrgRole Add Request");

            //get existing OrgRole
            var orgRoleQuery = new GetOrgRoleByID_Q
            {
                ID = orgRole.OrgRoleID
            };

            var existingOrgRole = _queryDispatcher.Dispatch<GetOrgRoleByID_Q, OrgRole>(context.AccessToken, orgRoleQuery);
            if (existingOrgRole == null)
                throw new Exception("Unable to retrive OrgRole for Unit-OrgRole Add Request");

            //check to ensure this OrgRole doesn't already belong to Unit
            var urQuery = new ListOrgRolesForUnit_Q
            {
                UnitID = unitID
            };

            var urResult = _queryDispatcher.Dispatch<ListOrgRolesForUnit_Q, List<UnitOrgRole>>(context.AccessToken, urQuery);
            var existingUnitOrgRole = urResult.Where(a => a.UnitID == unitID && a.OrgRoleID == orgRole.OrgRoleID);
            if (existingUnitOrgRole != null)
                throw new Exception("OrgRole already belongs to Unit");

            //add OrgRole to Unit
            var roleCmd = new AddOrgRoleToUnit_C
            {
                UnitID = existingUnit.ID,
                OrgRoleID = existingOrgRole.ID,
                IsActive = true,
                IsUnitLeadership = existingOrgRole.IsUnitLeadership,
                RatingCodeOverride = orgRole.RatingCodeOverride,
            };

            var roleResult = _commandDispatcher.Dispatch<AddOrgRoleToUnit_C>(context, roleCmd);
            if (roleResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error adding OrgRole to Unit in DB");
            }

            //update Mapped Ranks for UnitOrgRole
            var ranksCmd = new UpdateRanksForUnitOrgRole_C
            {
                OrgRoleID = existingOrgRole.ID,
                UnitID = unitID,
                SupportedRanks = orgRole.SupportedRanks.Select(a => a.ID).ToList()
            };

            var ranksResult = _commandDispatcher.Dispatch<UpdateRanksForUnitOrgRole_C>(context, ranksCmd);
            if (ranksResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error Updating UnitOrgRole Ranks in DB");
            }

            return roleResult;
        }

        public Result RemoveOrgRoleFromUnit(UserContext context, int unitID, int orgRoleID)
        {
            //get existing Unit
            var unitQuery = new GetUnitByID_Q
            {
                ID = unitID
            };

            var existingUnit = _queryDispatcher.Dispatch<GetUnitByID_Q, Unit>(context.AccessToken, unitQuery);
            if (existingUnit == null)
                throw new Exception("Unable to retrive Unit for Unit-OrgRole Delete Request");

            //get existing OrgRole
            var orgRoleQuery = new GetOrgRoleByID_Q
            {
                ID = orgRoleID
            };

            var existingOrgRole = _queryDispatcher.Dispatch<GetOrgRoleByID_Q, OrgRole>(context.AccessToken, orgRoleQuery);
            if (existingOrgRole == null)
                throw new Exception("Unable to retrive OrgRole for Unit-OrgRole Delete Request");
            
            //check to ensure this OrgRole already belongs to Unit
            var urQuery = new ListOrgRolesForUnit_Q
            {
                UnitID = unitID
            };

            var urResult = _queryDispatcher.Dispatch<ListOrgRolesForUnit_Q, List<UnitOrgRole>>(context.AccessToken, urQuery);
            var existingUnitOrgRole = urResult.Where(a => a.UnitID == unitID && a.OrgRoleID == orgRoleID);
            if (existingUnitOrgRole == null)
                throw new Exception("OrgRole doesn't belong to Unit");

            var cmd = new RemoveOrgRoleFromUnit_C
            {
                UnitID = unitID,
                OrgRoleID = orgRoleID
            };

            var result = _commandDispatcher.Dispatch<RemoveOrgRoleFromUnit_C>(context, cmd);
            if (result.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error Deleting UnitOrgRole in DB");
            }

            return result;

        }
    }
}
