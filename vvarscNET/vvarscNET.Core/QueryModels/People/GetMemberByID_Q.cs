using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Core.QueryModels.People
{
    public class GetMemberByID_Q : IQuery<Member>
    {
        public int MemberID;
    }
}
