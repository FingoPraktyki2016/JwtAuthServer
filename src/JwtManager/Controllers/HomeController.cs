using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserApps getUserApps)
            : base(managerSettings, loggerSettings, getUserApps)
        {
        }

        [AuthorizeFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}