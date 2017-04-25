using vvarscNET.Core.Service.Interfaces.QueryServices;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Core.QueryModels.Members;
using System;

namespace vvarscNET.Core.Service.QueryServices.Members
{
    public class MembersQueryService : IMembersQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public MembersQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public GetMemberByPID_QRM GetMemberByPID(string accessToken, string memberPID)
        {
            if (string.IsNullOrWhiteSpace(memberPID))
            {
                throw new ArgumentNullException(nameof(memberPID));
            }

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var query = new GetMemberByPID_Q
            {
                MemberPID = memberPID
            };
            return _queryDispatcher.Dispatch<GetMemberByPID_Q, GetMemberByPID_QRM>(accessToken, query);
        }
    }
}
