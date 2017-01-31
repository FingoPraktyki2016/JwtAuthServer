using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtManager.Controllers
{
    public class ApplicationController : BaseController
    {
        public ApplicationController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
        }

        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("index")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string s)
        {


            return View();
        }


        [AuthorizeFilter(UserRole.Manager)]
        [HttpGet("index")]
        public IActionResult Index()
        {


            return View();
        }


        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("adduser")]
        public IActionResult AddUser(string s)
        {

            return View();
        }


        [AuthorizeFilter(UserRole.Manager)]
        [HttpGet("adduser")]
        public IActionResult AddUser()
        {


            return View();
        }

        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("adduser")]
        public IActionResult EditUser(string s)
        {


            return View();
        }





    }
}
