using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtManager.Controllers
{
    public class AngularController : BaseController
    {
        public AngularController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserApps getUserApps,
            ISessionService<LoggedUserModel> loggedUserSessionService
        )
            : base(managerSettings, loggerSettings, getUserApps, loggedUserSessionService)
        {
            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
