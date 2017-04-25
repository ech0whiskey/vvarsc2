using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Members;

namespace vvarscNET.Core.QueryModels.Members
{
    public class GetMemberByAccessToken_Q : IQuery<GetMemberByAccessToken_QRM>
    {
        public string AccessTokenID;
    }
}
