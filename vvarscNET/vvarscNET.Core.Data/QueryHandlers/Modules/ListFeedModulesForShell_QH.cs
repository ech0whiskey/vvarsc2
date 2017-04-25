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
    public class ListFeedModulesForShell_QH : IQueryHandler<ListFeedModulesForShell_Q, List<ListFeedModulesForShell_QRM>>
    {
        private readonly SQLConnectionFactory _connFactory;

        public ListFeedModulesForShell_QH(SQLConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public List<ListFeedModulesForShell_QRM> Handle(string accessTokenID, ListFeedModulesForShell_Q query)
        {
            if (query.ShellID == 0)
                throw new ArgumentNullException(nameof(query.ShellID));

            using (var connection = _connFactory.GetConnection())
            {
                connection.Open();

                var sql = @"
                    select
	                    f.FeedPID
	                    ,f.[Guid] [FeedInstanceID]
	                    ,f.Title
	                    ,f.[Language]
	                    ,f.ShellID
	                    ,f.[Status]
	                    ,f.ShowLikes
	                    ,f.ShowComments
	                    ,f.ExpirationDays
	                    ,f.isAutoApproval [IsAutoApproval]
	                    ,f.AllowHTML
	                    ,f.CreatedOn
                    from Article.Feeds f
                    where f.ShellID = @ShellID
                    order by
	                    f.Title            
                ";

                var res = connection.Query<ListFeedModulesForShell_QRM>(sql, new { ShellID = query.ShellID }).ToList();

                return res;
            }
        }
    }
}
