using AutoMapper;
using DemoEmployeeApi.Core.Interface;
using DemoEmployeeApi.Core.Mappings;
using DemoEmployeeApi.Core.Models;
using DemoEmployeeApi.Repo.Data;
using DemoEmployeeApi.Repo.GenericRepository.Interface;
using DemoEmployeeApi.Repo.GenericRepository.Service;
using DemoEmployeeApi.Service.Interfaces;
using DemoEmployeeApi.Service.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace DemoEmployeeApi.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<RepositoryContext>(item => item.UseSqlServer(configuration.GetConnectionString("empCon")));
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IResponseModel, ResponseModel>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });           
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var identityAuthorityUrl = "https://localhost:7275";

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = identityAuthorityUrl;
                    options.ApiName = "identity-server-demo-api";
                });           
        }
    }
}
