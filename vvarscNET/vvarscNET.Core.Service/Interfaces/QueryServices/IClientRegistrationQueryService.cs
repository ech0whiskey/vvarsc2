using vvarscNET.Model.ResponseModels.Operations;

namespace vvarscNET.Core.Service.Interfaces.QueryServices
{
    public interface IClientRegistrationQueryService : IService
    {
        ClientRegistration GetClientRegistrationByAppName(string accessToken, string app);
    }
}
