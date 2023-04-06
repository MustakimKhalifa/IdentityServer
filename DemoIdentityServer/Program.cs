using DemoIdentityServer.Data;
using DemoIdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

//var seed = args.Contains("/seed");

//if (seed)
//{
//    args = args.Except(new[] { "/seed" }).ToArray();
//}

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("empCon");

//if (seed)
//{
    SeedData.EnsureSeedData(connectionString);
//}

builder.Services.ConfigureSqlContext(builder.Configuration);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("corsapp");

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();