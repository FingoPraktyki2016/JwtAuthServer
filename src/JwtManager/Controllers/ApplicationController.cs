using AutoMapper;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.Manager)]
    public class ApplicationController : BaseController
    {
        private readonly IGetApp getApp;

        public ApplicationController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetApp getApp)
            : base(managerSettings, loggerSettings)
        {
            this.getApp = getApp;
        }

        public IActionResult Index()
        {
            //TODO List of all user apps
            //  var models = new List<AppModel>( );  // TODO how to get all user apps from logged user

            var model = new AppViewModel()
            {
                Id = 1,
                Name = "App1"
            };

            //TODO A View with list of applications
            return View(new FormModel<AppViewModel>(false, model));

            //  return View();
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
            var app = getApp.Invoke(id);
            var model = new AppViewModel { Id = app.Id, Name = app.Name};

            return View(new FormModel<AppViewModel>(false, model));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Add()
        {
            var model = new AppViewModel();

            return View(model);
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AppViewModel model)
        {
            return View(model);
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Edit(int id)
        {
            var app = getApp.Invoke(id);
            var model = new AppViewModel { Id = app.Id, Name = app.Name };

            return View(new FormModel<AppViewModel>(true, model));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppViewModel model)
        {
            var viewModel = new FormModel<AppViewModel>(true, model);

            return View(viewModel);
        }
    }
}
