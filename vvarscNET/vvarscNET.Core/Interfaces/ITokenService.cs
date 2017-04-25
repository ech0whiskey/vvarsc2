using vvarscNET.Model.Security;

namespace vvarscNET.Core.Interfaces
{
    public interface ITokenService
    {
        bool ValidateTokenHash(string jwt);

        string EncodeJsonWebToken(JwtApiToken jwt, string privateKey);

        JwtApiToken GetJsonWebToken(string jwt);
    }
}
