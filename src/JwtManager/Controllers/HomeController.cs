using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<ManagerSettings> settings)
            : base(settings)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
