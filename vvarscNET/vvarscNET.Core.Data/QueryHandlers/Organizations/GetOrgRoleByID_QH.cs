﻿using Dapper;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.Organizations;
using System;
using System.Linq;
using System.Data.SqlClient;
using vvarscNET.Model.Objects.People;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.Organizations
{
    public class GetOrgRoleByID_QH : IQueryHandler<GetOrgRoleByID_Q, OrgRole>
    {
        private readonly SQLConnectionFactory _connFactory;

        public GetOrgRoleByID_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public OrgRole Handle(string accessTokenID, GetOrgRoleByID_Q query)
        {
            Dictionary<int, OrgRole> xRoles = new Dictionary<int, OrgRole>();
            Dictionary<int, HashSet<PayGrade>> xRolesPayGrades = new Dictionary<int, HashSet<PayGrade>>();

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
	                    ,g.ID
	                    ,g.PayGradeName
	                    ,g.PayGradeDisplayName
	                    ,g.PayGradeOrderBy
	                    ,g.PayGradeGroup
	                    ,g.IsActive
                    from Organizations.Roles r
                    join Organizations.PayGradeOrgRoleMap m
	                    on m.OrgRoleID = r.ID
                    join People.PayGrades g
	                    on g.ID = m.PayGradeID
                    where r.ID = @roleID          
                ";

                var res = connection.Query<OrgRole, PayGrade, OrgRole>(sql, (role, paygrade) =>
                {
                    if (!xRoles.ContainsKey(role.ID))
                        xRoles[role.ID] = role;

                    if (paygrade != null)
                    {
                        if (!xRolesPayGrades.ContainsKey(role.ID))
                            xRolesPayGrades[role.ID] = new HashSet<PayGrade>();
                        xRolesPayGrades[role.ID].Add(new PayGrade
                        {
                            ID = paygrade.ID,
                            PayGradeName = paygrade.PayGradeName,
                            PayGradeDisplayName = paygrade.PayGradeDisplayName,
                            PayGradeOrderBy = paygrade.PayGradeOrderBy,
                            PayGradeGroup = paygrade.PayGradeGroup,
                            IsActive = paygrade.IsActive
                        });
                    }

                    return role;
                }, new { roleID = query.ID }).ToList();

                foreach (var r in xRoles.Values)
                {
                    HashSet<PayGrade> payGrades = null;
                    xRolesPayGrades.TryGetValue(r.ID, out payGrades);
                    if (payGrades != null)
                        r.SupportedPayGrades = payGrades.ToList();
                }

                return xRoles.Values.FirstOrDefault();
            }
        }
    }
}
