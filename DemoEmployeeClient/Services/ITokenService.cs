using IdentityModel.Client;

namespace DemoEmployeeClient.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(string UserName, string Password);
    }
}
