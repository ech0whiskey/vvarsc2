using Dapper;
using vvarscNET.Core.QueryModels.Accounts;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.Accounts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.Accounts
{
    public class ListActiveShells_QH : IQueryHandler<ListActiveShells_Q, List<ListShells_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListActiveShells_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListShells_QRM> Handle(string accessTokenID, ListActiveShells_Q query)
        {

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    dbl.ShellID
	                    ,CAST(dbl.InstanceID as nvarchar(100)) [InstanceID]
                        ,s.ShellPID
	                    ,dbl.ShellName
	                    ,dbl.[Status]
	                    ,s.InsertDate [CreatedOn]
                    from Accounts.Shell.DBLookUp dbl
                    join Accounts.Shell.shells s
	                    on s.InstanceID = dbl.InstanceID
                    join Users.In_Owner ino
	                    on ino.InstanceID = s.InstanceID
                    where dbl.[Status] = 1
                    order by
	                    dbl.ShellName             
                ";

                var res = connection.Query<ListShells_QRM>(sql).ToList();

                return res;
            }
        }
    }
}
