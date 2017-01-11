using LegnicaIT.BussinesLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
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

        public IActionResult Index()
        {
            logger.Information("Action completed");
            return View();
        }
    }
}