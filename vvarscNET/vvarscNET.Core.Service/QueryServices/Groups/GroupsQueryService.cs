using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Groups;
using vvarscNET.Core.QueryModels.Groups;
using System;
using System.Collections.Generic;

namespace vvarscNET.Core.Service.QueryServices.Groups
{
    public class GroupsQueryService : IGroupsQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public GroupsQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<ListGroupsByShellAndGroupType_QRM> ListGroupsByShellAndGroupType(string accessTokenID, int shellID, string groupType)
        {
            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            var query = new ListGroupsByShellAndGroupType_Q
            {
                ShellID = shellID,
                GroupType = groupType
            };

            var result = _queryDispatcher.Dispatch<ListGroupsByShellAndGroupType_Q, List<ListGroupsByShellAndGroupType_QRM>>(accessTokenID, query);

            return result;
        }
    }
}
