using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
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
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.Manager)]
    public class ApplicationController : BaseController
    {
        private readonly IGetAppUsers getAppUsers;
        private readonly IGetAppUserRole getUserRole;
        private readonly IRevokeRole revokeRole;
        private readonly IGrantRole grantRole;
        private readonly IDeleteUserApp deleteUserApp;
        private readonly IGetUserApps getUserApps;
        private readonly IGetApp getApp;
        private readonly IAddNewApp addNewApp;
        private readonly IEditApp editApp;
        private readonly IAddNewUserApp addUserApp;
        private readonly IDeleteApp deleteApp;

        public ApplicationController(
            IGetAppUsers getAppUsers,
            IGetAppUserRole getUserRole,
            IRevokeRole revokeRole,
            IGrantRole grantRole,
            IDeleteUserApp deleteUserApp,
            IAddNewUserApp addUserApp,
            IGetUserApps getUserApps,
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetApp getApp,
            IAddNewApp addNewApp,
            IEditApp editApp,
            IDeleteApp deleteApp)
            : base(managerSettings, loggerSettings)
        {
            this.getAppUsers = getAppUsers;
            this.getUserRole = getUserRole;
            this.revokeRole = revokeRole;
            this.grantRole = grantRole;
            this.deleteUserApp = deleteUserApp;
            this.addUserApp = addUserApp;
            this.getApp = getApp;
            this.getUserApps = getUserApps;
            this.addNewApp = addNewApp;
            this.editApp = editApp;
            this.deleteApp = deleteApp;
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

            //TODO Pass data to views by view models (explicite: return View("Index", model)), not by ViewData[]
            ViewData["apps"] = listOfApps;

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
            }

            var newAppuser = new UserAppModel
            {
                AppId = appuser.AppId,
                UserId = appuser.UserId,
                Role = appuser.Role
            };

            addUserApp.Invoke(newAppuser);

            return View();
        }

        public IActionResult AddUser(int id)
        {
            //TODO Adduser View with action AddUser
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
            }

            deleteUserApp.Invoke(appuser.UserId);
            return View();
        }

        public IActionResult ListUsers(int appId)
        {
            var usersList = getAppUsers.Invoke(appId);

           //TODO Pass data to views by view models(explicite: return View("Index", model)), not by ViewData[]
        //   ViewData["users"] = usersList;

            return Json(usersList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RevokeUserRole(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
            }

            revokeRole.Invoke(appuser.AppId, appuser.UserId, appuser.Role);
            return RedirectToAction("ListUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GrantUserRole(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
            }

            grantRole.Invoke(appuser.AppId, appuser.UserId, appuser.Role);
            return RedirectToAction("ListUsers");
        }

        public IActionResult ChangeUserRole(int appId, int userId)
        {
            var userRole = getUserRole.Invoke(appId, userId);

            ViewData["userRole"] = userRole;

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
                Alert.Warning();
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };
            addNewApp.Invoke(newModel);

            // TODO: check if added
            Alert.Success();

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
                Alert.Warning();
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };
            editApp.Invoke(newModel);

            Alert.Success();
            return RedirectToAction("Details", new { id = newModel.Id });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            deleteApp.Invoke(id);
            // TODO: Return something in DeleteApp to check if deleted - bool?
            Alert.Success();

            return RedirectToAction("Index");
        }
    }
}
