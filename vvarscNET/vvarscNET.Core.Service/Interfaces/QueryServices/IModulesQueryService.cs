using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Modules;

namespace vvarscNET.Core.Service.Interfaces.QueryServices
{
    public interface IModulesQueryService
    {
        List<ListFeedModulesForShell_QRM> ListFeedModulesForShell(string accessToken, int ShellID);
        List<ListLibraryModulesForShell_QRM> ListLibraryModulesForShell(string accessToken, int ShellID);
    }
}