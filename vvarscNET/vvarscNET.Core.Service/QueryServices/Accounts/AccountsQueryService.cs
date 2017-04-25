using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Accounts;
using vvarscNET.Core.QueryModels.Accounts;
using System;
using System.Collections.Generic;

namespace vvarscNET.Core.Service.QueryServices.Accounts
{
    public class AccountsQueryService : IAccountsQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public AccountsQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<ListShells_QRM> ListActiveShells(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var query = new ListActiveShells_Q();

            return _queryDispatcher.Dispatch<ListActiveShells_Q, List<ListShells_QRM>>(accessToken, query);
        }
    }
}
