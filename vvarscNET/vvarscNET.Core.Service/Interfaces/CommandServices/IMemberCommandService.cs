using vvarscNET.Core.CommandModels.Members;
using vvarscNET.Model.RequestModels.Members;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces.CommandServices
{
    public interface IMemberCommandService
    {
        Result UpdateMember(UserContext context, UpdateMemberRequestModel member);
    }
}
