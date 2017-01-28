using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<ManagerSettings> managerSettings, IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}