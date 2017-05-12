using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.ResponseModels.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class ListRanks_QH : IQueryHandler<ListRanks_Q, List<ListRanks_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListRanks_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListRanks_QRM> Handle(string accessTokenID, ListRanks_Q query)
        {
            Dictionary<int, ListRanks_QRM> xRanks = new Dictionary<int, ListRanks_QRM>();
            Dictionary<int, HashSet<OrgRole>> xRanksOrgRoles = new Dictionary<int, HashSet<OrgRole>>();

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    r.ID [RankID]
	                    ,r.PayGradeID
	                    ,pg.PayGradeName
	                    ,pg.PayGradeDisplayName
	                    ,pg.PayGradeOrderBy
	                    ,pg.PayGradeGroup
                        ,pg.PayGradeDescriptionText
                        ,pg.PayGradeNotes
	                    ,r.RankName
	                    ,r.RankAbbr
	                    ,r.RankType
	                    ,r.RankImage
	                    ,r.RankGroupName
	                    ,r.RankGroupImage
						,mr.ID
						,mr.RoleName
						,mr.RoleShortName
						,mr.RoleDisplayName
						,mr.RoleType
						,mr.RoleOrderBy
						,mr.IsActive
						,mr.IsHidden
                    from People.Ranks r
                    join People.PayGrades pg
	                    on pg.ID = r.PayGradeID
					outer apply (
						select
							r.*
						from People.PayGradeOrgRoleMap m
						join Organizations.Roles r
							on r.ID = m.OrgRoleID
						where m.PayGradeID = pg.ID
					) mr
                    order by
	                    pg.PayGradeOrderBy
	                    ,r.RankType
	                    ,r.RankName          
                ";

                var res = connection.Query<ListRanks_QRM, OrgRole, ListRanks_QRM>(sql, (rank, role) =>
                {
                    if (!xRanks.ContainsKey(rank.RankID))
                        xRanks[rank.RankID] = rank;

                    if (role != null)
                    {
                        if (!xRanksOrgRoles.ContainsKey(rank.RankID))
                            xRanksOrgRoles[rank.RankID] = new HashSet<OrgRole>();
                        xRanksOrgRoles[rank.RankID].Add(new OrgRole
                        {
                            ID = role.ID,
                            RoleName = role.RoleName,
                            RoleShortName = role.RoleShortName,
                            RoleDisplayName = role.RoleDisplayName,
                            RoleType = role.RoleType,
                            IsActive = role.IsActive,
                            IsHidden = role.IsHidden
                        });
                    }
                    return rank;
                }).ToList();

                foreach (var r in xRanks.Values)
                {
                    HashSet<OrgRole> orgRoles = null;
                    xRanksOrgRoles.TryGetValue(r.RankID, out orgRoles);
                    if (orgRoles != null)
                        r.SupportedOrgRoles = orgRoles.ToList();
                }

                return xRanks.Values.ToList();
            }
        }
    }
}
