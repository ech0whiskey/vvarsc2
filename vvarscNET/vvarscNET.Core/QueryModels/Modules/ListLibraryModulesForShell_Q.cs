using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Modules;
using System.Collections.Generic;

namespace vvarscNET.Core.QueryModels.Modules
{
    public class ListLibraryModulesForShell_Q : IQuery<List<ListLibraryModulesForShell_QRM>>
    {
        public int ShellID;
    }
}
