using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Modules;
using System.Collections.Generic;

namespace vvarscNET.Core.QueryModels.Modules
{
    public class ListFeedModulesForShell_Q : IQuery<List<ListFeedModulesForShell_QRM>>
    {
        public int ShellID;
    }
}
