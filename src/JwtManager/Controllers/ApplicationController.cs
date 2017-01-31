using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.JwtManager.Models;
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

        public IActionResult Index()
        {
            var userApps = getUserApps.Invoke(LoggedUser.Id);

            //TODO A View with list of applications
            //  return View(new FormModel<AppModel>(false,userApps));
            return Json(userApps);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserAppModel appuser)
        {
            //TODO Add new app user. Go to Index or refresh view?
            return View();
        }

        public IActionResult AddUser()
        {
            //TODO A view with User text boxes string email,name and int id
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(UserAppModel appuser)
        {
            //TODO Go to Index or refresh view?
            return View();
        }

        public IActionResult Listusers() // Based on selected app?
        {
            //TODO A view with User text boxes string email,name and int id
            return View();
        }

        /*
         *  Show/add/edit applications
         */

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Details(int id)
        {
            var model = new AppModel() { Id = 5, Name = "bla" };

            return View(new FormModel<AppModel>(false, model));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Add()
        {
            var model = new AppModel();

            return View(model);
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AppModel model)
        {
            return View(model);
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Edit(int id)
        {
            var model = new AppModel();
            var viewModel = new FormModel<AppModel>(true, model);

            return View(viewModel);
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppModel model)
        {
            var viewModel = new FormModel<AppModel>(true, model);

            return View(viewModel);
        }
    }
}
