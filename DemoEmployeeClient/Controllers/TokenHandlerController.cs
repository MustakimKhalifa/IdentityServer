using DemoEmployeeClient.Models;
using DemoEmployeeClient.Services;
using Google.Apis.Auth.OAuth2.Requests;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace DemoEmployeeClient.Controllers
{
    public class TokenHandlerController : Controller
    {
        private ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenHandlerController(ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IActionResult> Index()
        {      
            //get cookies value
            var username = _httpContextAccessor.HttpContext.Request.Cookies["username"];
            var password = _httpContextAccessor.HttpContext.Request.Cookies["password"];

            var tokenResponse = await _tokenService.GetTokenAsync(username, password);

            //Remove cookies value
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("password");

            //save token into cookie
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("token", tokenResponse.AccessToken, options);
            
            return RedirectToAction("CallApi");
        }

        public IActionResult CallApi()
        {
            return View();
        }
    }
}
