using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using Microsoft.AspNetCore.Mvc;
using LegnicaIT.JwtManager.Models;

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

        //[HttpGet("index")]
        public IActionResult Index()
        {


            //TODO List of all user apps
            //  var models = new List<AppModel>( );  // TODO how to get all user apps from logged user


            var model = new AppModel()
            {
                Id = 1,
                Name = "App1"
            };

            //TODO A View with list of applications
            return View(new FormModel<AppModel>(false,model));

          //  return View();
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
