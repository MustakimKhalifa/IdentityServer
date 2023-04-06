
using DemoIdentityServer.Configuration;
using DemoIdentityServer.Data;
using DemoIdentityServer.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DemoIdentityServer.Extensions
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                   options =>
                   {
                       options.ConfigureDbContext = b =>
                            b.UseSqlServer(connectionString, x => x.MigrationsAssembly("DemoIdentityServer"));
                   });

            services.AddConfigurationDbContext(
                   options =>
                   {
                       options.ConfigureDbContext = b =>
                            b.UseSqlServer(connectionString, x => x.MigrationsAssembly("DemoIdentityServer"));
                   });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<ApplicationDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var angella = userMgr.FindByNameAsync("angella").Result;
            if (angella == null)
            {
                angella = new ApplicationUser
                {
                    UserName = "angella",
                    Email = "angella@gmail.com",
                    EmailConfirmed = true,
                };

                var result = userMgr.CreateAsync(angella, "Angella@123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(angella,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name,"angella"),
                        new Claim("location","somewhere"),
                    }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            //if (!context.Clients.Any())
            //{
            Console.WriteLine("Clients being populated");
            foreach (var client in Config.Clients)
            {
                var item = context.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);

                if (item == null)
                {
                    context.Clients.Add(client.ToEntity());
                    context.SaveChanges();
                }
            }
            //}
            //else
            //{
            //    Console.WriteLine("Clients already populated");
            //}

            //if (!context.IdentityResources.Any())
            //{
            Console.WriteLine("IdentityResources being populated");
            foreach (var resource in Config.IdentityResources)
            {
                var item = context.IdentityResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                    context.SaveChanges();
                }
            }

            //}
            //else
            //{
            //    Console.WriteLine("IdentityResources already populated");
            //}

            //if (!context.ApiResources.Any())
            //{
            Console.WriteLine("ApiResources being populated");
            foreach (var resource in Config.ApiResources)
            {
                var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.ApiResources.Add(resource.ToEntity());
                    context.SaveChanges();
                }
            }
            //            }
            //            else
            //{
            //    Console.WriteLine("ApiResources already populated");
            //}

            // if (!context.ApiScopes.Any())
            //{
            Console.WriteLine("Scopes being populated");
            foreach (var scope in Config.ApiScopes)
            {
                var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                if (item == null)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                    context.SaveChanges();
                }
            }
            // }
            //else
            //{
            //    Console.WriteLine("Scopes already populated");
            //}

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }
}
