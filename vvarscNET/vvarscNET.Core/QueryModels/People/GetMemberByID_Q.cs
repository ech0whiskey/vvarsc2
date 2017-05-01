using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.People;

namespace vvarscNET.Core.QueryModels.People
{
    public class GetMemberByID_Q : IQuery<GetMemberByID_QRM>
    {
        public string MemberID;
    }
}
