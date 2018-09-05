using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Core.QueryModels.People;

namespace vvarscNET.Core.Service.QueryServices
{
    public class PeopleQueryService : IPeopleQueryService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PeopleQueryService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public List<Member> ListMembers(string accessToken)
        {
            var query = new ListMembers_Q();

            var result = _queryDispatcher.Dispatch<ListMembers_Q, List<Member>>(accessToken, query);

            return result;
        }
        
        public Member GetMemberByID(string accessToken, int memberID)
        {
            var query = new GetMemberByID_Q
            {
                MemberID = memberID
            };

            var result = _queryDispatcher.Dispatch<GetMemberByID_Q, Member>(accessToken, query);

            return result;
        }
    }
}
