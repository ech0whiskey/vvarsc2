using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Modules;
using vvarscNET.Core.QueryModels.Modules;
using System;
using System.Collections.Generic;

namespace vvarscNET.Core.Service.QueryServices.Modules
{
    public class ModulesQueryService : IModulesQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public ModulesQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<ListFeedModulesForShell_QRM> ListFeedModulesForShell(string accessTokenID, int shellID)
        {
            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            var query = new ListFeedModulesForShell_Q
            {
                ShellID = shellID
            };

            var result = _queryDispatcher.Dispatch<ListFeedModulesForShell_Q, List<ListFeedModulesForShell_QRM>>(accessTokenID, query);

            return result;
        }

        public List<ListLibraryModulesForShell_QRM> ListLibraryModulesForShell(string accessTokenID, int shellID)
        {
            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            var query = new ListLibraryModulesForShell_Q
            {
                ShellID = shellID
            };

            var result = _queryDispatcher.Dispatch<ListLibraryModulesForShell_Q, List<ListLibraryModulesForShell_QRM>>(accessTokenID, query);

            return result;
        }
    }
}
