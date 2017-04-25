using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Members;

namespace vvarscNET.Core.QueryModels.Members
{
    public class GetMemberByPID_Q : IQuery<GetMemberByPID_QRM>
    {
        public string MemberPID;
    }
}
