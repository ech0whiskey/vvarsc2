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
    public class ListOrgRoles_QH : IQueryHandler<ListOrgRoles_Q, List<OrgRole>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListOrgRoles_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<OrgRole> Handle(string accessTokenID, ListOrgRoles_Q query)
        {
            Dictionary<int, OrgRole> xRoles = new Dictionary<int, OrgRole>();
            Dictionary<int, HashSet<Rank>> xRolesRanks = new Dictionary<int, HashSet<Rank>>();

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    r.ID
	                    ,r.OrganizationID
	                    ,r.RoleName
	                    ,r.RoleDisplayName
	                    ,r.RoleType
	                    ,r.RoleOrderBy
	                    ,r.IsActive
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
                    from Organizations.OrgRoles r
                    join Organizations.PayGradeOrgRoleMap m
	                    on m.OrgRoleID = r.ID
                    join Organizations.Ranks r2
                        on r2.RankID = m.RankID        
                ";

                var res = connection.Query<OrgRole, Rank, OrgRole>(sql, (role, rank) => 
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
                }).ToList();

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
