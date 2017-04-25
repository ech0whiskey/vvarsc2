using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Groups;

namespace vvarscNET.Core.Service.Interfaces.QueryServices
{
    public interface IGroupsQueryService
    {
        List<ListGroupsByShellAndGroupType_QRM> ListGroupsByShellAndGroupType(string accessToken, int shellID, string groupType);
    }
}