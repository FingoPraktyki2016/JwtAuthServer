using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
