using Microsoft.AspNetCore.Mvc;

namespace DemoIdentityServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
