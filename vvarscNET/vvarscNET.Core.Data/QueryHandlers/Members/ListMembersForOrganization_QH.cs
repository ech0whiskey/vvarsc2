﻿using Dapper;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.Objects.People;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace vvarscNET.Core.Data.QueryHandlers.People
{
    public class ListMembersForOrganization_QH : IQueryHandler<ListMembersForOrganization_Q, List<Member>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListMembersForOrganization_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<Member> Handle(string accessTokenID, ListMembersForOrganization_Q query)
        {
            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    m.ID
	                    ,m.UserName
	                    ,m.RSIHandle
	                    ,m.OrganizationID
                        ,m.UserType
	                    ,m.IsActive
	                    ,m.CreatedOn
	                    ,m.CreatedBy
	                    ,m.ModifiedOn
	                    ,m.ModifiedBy
                    from [People].[Members] m
                    where m.OrganizationID = @OrganizationID            
                ";

                var res = connection.Query<Member>(sql, new { OrganizationID = query.OrganizationID }).ToList();

                return res;
            }
        }
    }
}
