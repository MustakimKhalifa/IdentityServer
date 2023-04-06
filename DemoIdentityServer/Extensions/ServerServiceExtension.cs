using DemoIdentityServer.Data;
using DemoIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoIdentityServer.Extensions
{
    public static class ServerServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("empCon"), x => x.MigrationsAssembly("DemoIdentityServer")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("empCon"), x => x.MigrationsAssembly("DemoIdentityServer"));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("empCon"), x => x.MigrationsAssembly("DemoIdentityServer"));
                    });
        }
    }
}
