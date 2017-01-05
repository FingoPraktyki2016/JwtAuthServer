using Microsoft.AspNetCore.Mvc;

namespace JwtManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
