using IdentityServer4;
using IdentityServer4.Models;

namespace DemoIdentityServer.Configuration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "name",
                    UserClaims = new List<string>
                    {
                        "role"
                    }
                }
            };

        public static List<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "mk",
                AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                RequireClientSecret = false,
                RequireConsent = false,
                RedirectUris = new List<string> { "https://localhost:7254/TokenHandler/callback.html" },
                PostLogoutRedirectUris = new List<string> { "https://localhost:7275/Account/Login" },
                AllowedScopes = { "read" },
                //AllowedCorsOrigins = new List<string>
                //{
                //    "http://localhost:3006",
                //},
                //AccessTokenLifetime = 86400
            },
            new Client
            {
                ClientId = "identity-server-demo-web",
                AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                RequireClientSecret = false,
                RequireConsent = false,
                RedirectUris = new List<string> { "http://localhost:3006/signin-callback.html" },
                PostLogoutRedirectUris = new List<string> { "http://localhost:3006/" },
                AllowedScopes = { "identity-server-demo-api", "write", "read", "openid", "profile", "email" },
                //AllowedCorsOrigins = new List<string>
                //{
                //    "http://localhost:3006",
                //},
                AccessTokenLifetime = 86400
            },
            new Client
            {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "read" }
            },
             new Client
            {
                ClientId = "client1",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "openid" }
            },
             new Client
            {
                ClientId = "client2",
                ClientName = "Client2",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("clientsecret".Sha256())
                },
                AllowedScopes = { "read","write" }
            },
             new Client
            {
                ClientId = "mustakim",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("mustakimsecret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "openid", "read", "write" }
            },
        };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "identity-server-demo-api",
                DisplayName = "Identity Server Demo API",
                Scopes = new List<string>
                {
                    "write",
                    "read",
                    "openid"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            //new ApiScope("openid"),
            //new ApiScope("profile"),
            new ApiScope("email"),
            new ApiScope("read"),
            new ApiScope("write"),
            new ApiScope("identity-server-demo-api")
        };
    }
}
