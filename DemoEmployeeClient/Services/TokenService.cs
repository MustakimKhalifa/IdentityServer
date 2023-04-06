using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace DemoEmployeeClient.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> _IdentityServerSettings;
        public readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<IdentityServerSettings> IdentityServerSettings)
        {
            _IdentityServerSettings = IdentityServerSettings;
            _httpClient = new HttpClient();
            _discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(_IdentityServerSettings.Value.DiscoveryUrl).Result;
            
            if( _discoveryDocument.IsError)
            {
                throw new Exception("Unable to get discovery document");  
            }
        }

        public async Task<TokenResponse> GetTokenAsync(string UserName, string Password)
        {
            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                ClientId = _IdentityServerSettings.Value.ClientName,
                ClientSecret = _IdentityServerSettings.Value.ClientPassword,
                Scope = _IdentityServerSettings.Value.Scope,
                UserName = UserName,
                Password = Password
            });

            if ( tokenResponse.IsError )
            {
                throw new Exception("unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
