﻿using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.Extensions.Options;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.Manager)]
    public class ApplicationController : BaseController
    {
        private readonly IGetUserApps getUserApps;
        private readonly IGetApp getApp;
        private readonly IAddNewApp addNewApp;
        private readonly IEditApp editApp;
        private readonly IAddNewUserApp addUserApp;

        public ApplicationController(
            IAddNewUserApp addUserApp,
            IGetUserApps getUserApps,
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetApp getApp,
            IAddNewApp addNewApp,
            IEditApp editApp)
            : base(managerSettings, loggerSettings)
        {
            this.addUserApp = addUserApp;
            this.getApp = getApp;
            this.getUserApps = getUserApps;
            this.addNewApp = addNewApp;
            this.editApp = editApp;
        }

        public IActionResult Index()
        {
            var userApps = getUserApps.Invoke(LoggedUser.UserModel.Id);
            List<AppViewModel> listOfApps = new List<AppViewModel>();

            foreach (var appFromDb in userApps)
            {
                var model = new AppViewModel
                {
                    Id = appFromDb.Id,
                    Name = appFromDb.Name
                };

                listOfApps.Add(model);
            }

            ViewData["apps"] = listOfApps;

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                //TODO error info
            }
            var newAppuser = new LegnicaIT.BusinessLogic.Models.UserAppModel
            {
                AppId = appuser.AppId,
                UserId = appuser.UserId,
                Role = appuser.Role
            };

            addUserApp.Invoke(newAppuser);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(AppUserViewModel appuser)
        {
            // deleteUserApp.Invoke();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(UserAppModel appuser)
        {
            //TODO Adduser View with action AddUser
            return View();
        }

        public IActionResult Listusers() // Based on selected app?
        {
            return View();
        }

        /*
         *  Show/add/edit applications
         */

        [AuthorizeFilter(UserRole.User)]
        public IActionResult Details(int id)
        {
            var app = getApp.Invoke(id);
            var model = new AppViewModel { Id = app.Id, Name = app.Name };

            return View(new FormModel<AppViewModel>(model));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public IActionResult Add()
        {
            var model = new AppViewModel();

            return View(new FormModel<AppViewModel>(model, true));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AppViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Alert danger
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };
            addNewApp.Invoke(newModel);

            // TODO: Alert success
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var app = getApp.Invoke(id);

            var model = new AppViewModel { Id = app.Id, Name = app.Name };

            return View(new FormModel<AppViewModel>(model, true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Alert danger
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };
            editApp.Invoke(newModel);

            // TODO: Alert success
            return RedirectToAction("Details", new { id = newModel.Id });
        }
    }
}
