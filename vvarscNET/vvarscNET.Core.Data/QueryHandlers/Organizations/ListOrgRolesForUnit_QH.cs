using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.Organizations
{
    public class ListOrgRolesForUnit_QH : IQueryHandler<ListOrgRolesForUnit_Q, List<UnitOrgRole>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListOrgRolesForUnit_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<UnitOrgRole> Handle(string accessTokenID, ListOrgRolesForUnit_Q query)
        {
            Dictionary<int, UnitOrgRole> xRoles = new Dictionary<int, UnitOrgRole>();
            Dictionary<int, HashSet<Rank>> xRolesRanks = new Dictionary<int, HashSet<Rank>>();

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    um.ID
	                    ,um.UnitID
	                    ,um.OrgRoleID
	                    ,r.RoleName
	                    ,r.RoleShortName
	                    ,r.RoleDisplayName
	                    ,r.RoleType
	                    ,um.RatingCodeOverride
	                    ,r.IsUnitLeadership
	                    ,um.IsActive
	                    ,r.IsHidden
	                    ,r2.ID
	                    ,r2.PayGradeID
	                    ,r2.RankName
	                    ,r2.RankAbbr
	                    ,r2.RankType
                        ,r2.RankImage
                        ,r2.RankGroupName
                        ,r2.RankGroupImage
                        ,r2.RatingCodeSuffix
	                    ,r2.IsActive
                    from Organizations.UnitOrgRoleMap um
                    join Organizations.OrgRoles r
	                    on r.ID = um.OrgRoleID
                    join Organizations.RankOrgRoleMap m2
	                    on m2.OrgRoleID = r.ID
                    join Organizations.Ranks r2
                        on r2.ID = m2.RankID 
                    where um.UnitID = @UnitID       
                ";

                var res = connection.Query<UnitOrgRole, Rank, UnitOrgRole>(sql, (role, rank) => 
                {
                    if (!xRoles.ContainsKey(role.ID))
                        xRoles[role.ID] = role;

                    if (rank != null)
                    {
                        if (!xRolesRanks.ContainsKey(role.ID))
                            xRolesRanks[role.ID] = new HashSet<Rank>();
                        xRolesRanks[role.ID].Add(new Rank
                        {
                            ID = rank.ID,
                            PayGradeID = rank.PayGradeID,
                            RankName = rank.RankName,
                            RankAbbr = rank.RankAbbr,
                            RankType = rank.RankType,
                            RankImage = rank.RankImage,
                            RankGroupName = rank.RankGroupName,
                            RankGroupImage = rank.RankGroupImage,
                            RatingCodeSuffix = rank.RatingCodeSuffix,
                            IsActive = rank.IsActive
                        });
                    }

                    return role;
                }, new { UnitID = query.UnitID }).ToList();

                foreach (var r in xRoles.Values)
                {
                    HashSet<Rank> ranks = null;
                    xRolesRanks.TryGetValue(r.ID, out ranks);
                    if (ranks != null)
                        r.SupportedRanks = ranks.ToList();
                }

                return xRoles.Values.ToList();
            }
        }
    }
}
