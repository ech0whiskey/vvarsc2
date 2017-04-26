using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Members;

namespace vvarscNET.Core.QueryModels.Members
{
    public class GetMemberByID_Q : IQuery<GetMemberByID_QRM>
    {
        public string MemberID;
    }
}
