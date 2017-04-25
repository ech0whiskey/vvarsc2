using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Accounts;

namespace vvarscNET.Core.Service.Interfaces.QueryServices
{
    public interface IAccountsQueryService
    {
        List<ListShells_QRM> ListActiveShells(string accessToken);
    }
}