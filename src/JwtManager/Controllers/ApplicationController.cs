using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.Manager)]
    public class ApplicationController : BaseController
    {
        public ApplicationController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("adduser")]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(string s)
        {
            return View();
        }

        [HttpGet("adduser")]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost("edituser")]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(string s)
        {
            return View();
        }
    }
}
