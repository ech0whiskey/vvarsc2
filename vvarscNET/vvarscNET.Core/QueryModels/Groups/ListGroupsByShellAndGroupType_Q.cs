using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Groups;
using System.Collections.Generic;

namespace vvarscNET.Core.QueryModels.Groups
{
    public class ListGroupsByShellAndGroupType_Q : IQuery<List<ListGroupsByShellAndGroupType_QRM>>
    {
        public int ShellID;
        public string GroupType;
    }
}
