using DemoEmployeeClient.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

namespace DemoEmployeeClient.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void RegisterTokenHandler(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme =CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme=OpenIdConnectDefaults.AuthenticationScheme;

                options.Authority = configuration["InteractiveServiceSettings:AuthorityUrl"];// Auth Server  
                options.ClientId = configuration["InteractiveServiceSettings:ClienId"]; // client setup in Auth Server  
                options.ClientSecret = configuration["InteractiveServiceSettings:ClientSecret"];                
                options.ResponseType = "code"; 
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                //options.RequireHttpsMetadata = false; // only for development   
                //options.Scope.Add("openid");
                //options.Scope.Add("write");
            });

            services.AddMvc();
        }
    }
}
