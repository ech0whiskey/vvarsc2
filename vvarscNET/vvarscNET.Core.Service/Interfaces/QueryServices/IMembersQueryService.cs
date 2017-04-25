using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.Members;

namespace vvarscNET.Core.Service.Interfaces.QueryServices
{
    public interface IMembersQueryService
    {
        GetMemberByPID_QRM GetMemberByPID(string accessTokenID, string memberPID);
    }
}