using Dapper;
using vvarscNET.Core.QueryModels.Modules;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Factories;
using vvarscNET.Model.ResponseModels.Modules;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace vvarscNET.Core.Data.QueryHandlers.Modules
{
    public class ListLibraryModulesForShell_QH : IQueryHandler<ListLibraryModulesForShell_Q, List<ListLibraryModulesForShell_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListLibraryModulesForShell_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListLibraryModulesForShell_QRM> Handle(string accessTokenID, ListLibraryModulesForShell_Q query)
        {
            if (query.ShellID == 0)
                throw new ArgumentNullException(nameof(query.ShellID));

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    dbl.ShellID
	                    ,dl.DocLibraryID
	                    ,dl.LibraryPID
	                    ,dl.SiteID [LibraryInstanceID]
	                    ,dl.DocLibraryTitle
	                    ,dl.CreateDate [CreatedOn]
	                    ,dl.ModifyDate [ModifiedOn]
                    from Accounts.Shell.DBLookUp dbl
                    join Accounts.Company.Company c
	                    on c.CompanyGuid = dbl.InstanceID
                    join Asset.DocLibrary dl
	                    on dl.CompanyID = c.CompanyID
                    where dbl.ShellID = @ShellID
	                    and dl.ListType = 100
                    order by
	                    dl.DocLibraryTitle            
                ";

                var res = connection.Query<ListLibraryModulesForShell_QRM>(sql, new { ShellID = query.ShellID }).ToList();

                return res;
            }
        }
    }
}
