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

        public ActionResult Index()
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
        public ActionResult AddUser(AppUserViewModel appuser)
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

            return RedirectToAction("ListUsers");
        }

        public ActionResult AddUser(int appId)
        {
            ViewData["appId"] = appId;

            //TODO Adduser View with action AddUser
            return View("AddUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int userId, int appId)
        {
            if (deleteUserApp.Invoke(userId, appId))
            {
                Alert.Success();
            }
            else
            {
                Alert.Danger("Something went wrong");
            }

            return RedirectToAction("Details", new { id = appId});
        }

        public ActionResult ListUsers(int appId = 4) //TODO for tests
        {
            var usersList = getAppUsers.Invoke(appId);

            List<UserDetailsFromAppViewModel> listOfUsers = new List<UserDetailsFromAppViewModel>();

            foreach (var user in usersList)
            {
                var model = new UserDetailsFromAppViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                };

                listOfUsers.Add(model);
            }

            //TODO Pass data to views by view models(explicite: return View("Index", model)), not by ViewData[]
            ViewData["appId"] = appId;
            ViewData["users"] = listOfUsers;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RevokeUserRole(AppUserViewModel appuser)
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
        public ActionResult GrantUserRole(AppUserViewModel appuser)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
            }

            grantRole.Invoke(appuser.AppId, appuser.UserId, appuser.Role);
            return RedirectToAction("ListUsers");
        }

         public IActionResult ChangeUserRole(int appId =1, int userId=1) //for tests
        {
           var userRole = getUserRole.Invoke(appId, userId);

            var model = new AppUserViewModel()
            {
                AppId=appId,
                UserId = userId,
                Role = userRole
            };

            return View(model);
        }

        /*
         *  Show/add/edit applications
         */

        [AuthorizeFilter(UserRole.User)]
        public ActionResult Details(int id)
        {
            var app = getApp.Invoke(id);
            var model = new AppViewModel { Id = app.Id, Name = app.Name };

            ViewData.Add("listUser", ListUsers(id));

            return View(new FormModel<AppViewModel>(model));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public ActionResult Add()
        {
            var model = new AppViewModel();

            return View(new FormModel<AppViewModel>(model, true));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AppViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };

            if (addNewApp.Invoke(newModel) != 0)
            {
                Alert.Success();
            }
            else
            {
                Alert.Danger("Something went wrong");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var app = getApp.Invoke(id);
            var model = new AppViewModel { Id = app.Id, Name = app.Name };

            return View(new FormModel<AppViewModel>(model, true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
                return View(new FormModel<AppViewModel>(model, true));
            }

            var newModel = new AppModel { Id = model.Id, Name = model.Name };

            if (editApp.Invoke(newModel))
            {
                Alert.Success();
            }
            else
            {
                Alert.Danger("Something went wrong");
            }

            return RedirectToAction("Details", new { id = newModel.Id });
        }

        // FIXME:
        //[HttpDelete]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (deleteApp.Invoke(id))
            {
                Alert.Success();
            }
            else
            {
                Alert.Danger("Something went wrong");
            }

            return RedirectToAction("Index");
        }
    }
}
