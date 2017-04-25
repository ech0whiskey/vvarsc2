using Dapper;
using vvarscNET.Core.QueryModels.Groups;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.Groups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.Groups
{
    public class ListGroupsByShellAndGroupType_QH : IQueryHandler<ListGroupsByShellAndGroupType_Q, List<ListGroupsByShellAndGroupType_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListGroupsByShellAndGroupType_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListGroupsByShellAndGroupType_QRM> Handle(string accessTokenID, ListGroupsByShellAndGroupType_Q query)
        {
            if (query.ShellID == 0)
                throw new ArgumentNullException(nameof(query.ShellID));

            if (string.IsNullOrEmpty(query.GroupType))
                throw new ArgumentNullException(nameof(query.GroupType));

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    ino.ShellID
	                    ,ino.OwnerID [GroupID]
	                    ,g.InstanceID [GroupInstanceID]
	                    ,g.GroupPID
	                    ,g.GroupName
	                    ,g.DisplayGroupName
	                    ,g.[Status]
	                    ,lk1.[Description] [GroupType]
	                    ,g.Cust_GroupID
	                    ,g.InsertDate [CreatedOn]
                    from Users.Groups g
                    join Users.In_Owner ino
	                    on ino.InstanceID = g.InstanceID
                    join Lkp.LK_GroupTypes lk1
	                    on lk1.GroupType = g.GroupType
	                    and lk1.[Description] = @GroupType
                    where ino.ShellID = @ShellID
                    order by
	                    g.GroupName           
                ";

                var res = connection.Query<ListGroupsByShellAndGroupType_QRM>(sql, new {
                    ShellID = query.ShellID,
                    GroupType = query.GroupType
                }).ToList();

                return res;
            }
        }
    }
}
