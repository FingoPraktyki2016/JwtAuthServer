using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.Manager)]
    public class ApplicationController : BaseController
    {
        private readonly IGetUserApps getUserApps;

        public ApplicationController(
            IGetUserApps getUserApps,          
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
             this.getUserApps = getUserApps;
    }

        //[HttpGet("index")]
        public IActionResult Index()
        {
            var userApps = getUserApps.Invoke(LoggedUser.Id);

            //TODO A View with list of applications
            //  return View(new FormModel<AppModel>(false,userApps));
            return Json(userApps);
            
        }

        [HttpPost("adduser")]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserAppModel appuser)
        {
            //TODO Add new app user. Go to Index or refresh view?
            return View();
        }

        [HttpGet("adduser")]
        public IActionResult AddUser( )
        {
            //TODO A view with User text boxes string email,name and int id
            return View();
        }

        [HttpPost("edituser")]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(UserAppModel appuser)
        {
            //TODO Go to Index or refresh view?
            return View();
        }

        [HttpGet("listusers")]
        public IActionResult Listusers() // Based on selected app?
        {
            //TODO A view with User text boxes string email,name and int id
            return View();
        }

    }
}
