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
using LegnicaIT.JwtManager.Services.Interfaces;

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
        private readonly ICheckUserPermission checkUserPermission;
        private readonly ICheckUserPermissionToApp checkUserPermissionToApp;

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
            IDeleteApp deleteApp,
            ICheckUserPermission checkUserPermission,
            ICheckUserPermissionToApp checkUserPermissionToApp,
            ISessionService<LoggedUserModel> loggedUserSessionService)
            : base(managerSettings, loggerSettings, getUserApps, loggedUserSessionService)
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
            this.checkUserPermission = checkUserPermission;
            this.checkUserPermissionToApp = checkUserPermissionToApp;

            Breadcrumb.Add("Application", "Index", "Application");
        }

        [AuthorizeFilter(UserRole.None)]
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
        [AuthorizeFilter(UserRole.None)]
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
            Breadcrumb.Add("Add user", "AddUser", "Application");
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

            return RedirectToAction("Details", new { id = appId });
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
        public IActionResult ChangeUserRole(int appId, int userId, UserRole oldRole, UserRole role)
        {
            if (role > oldRole)
            {
                grantRole.Invoke(appId, userId, role);
            }
            else if (role < oldRole)
            {
                revokeRole.Invoke(appId, userId, role);
            }

            return RedirectToAction("ListUsers");
        }

        public IActionResult ChangeUserRole(int appId , int userId )
        {
            Breadcrumb.Add("Change user role", "ChangeUserRole", "Application");

            var userRole = getUserRole.Invoke(appId, userId);

            var model = new AppUserViewModel()
            {
                AppId = appId,
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
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id))
            {
                Breadcrumb.Add("Application details", "Details", "Application");

            var app = getApp.Invoke(id);

            if (app == null)
            {
                return View("Error");
            }

            var model = new AppViewModel { Id = app.Id, Name = app.Name };

                ViewData.Add("listUser", ListUsers(id));

                return View(new FormModel<AppViewModel>(model));
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        public ActionResult Add()
        {
            Breadcrumb.Add("Add application", "Add", "Application");

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
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id))
            {
                Breadcrumb.Add("Edit application", "Edit", "Application");

                var app = getApp.Invoke(id);
                var model = new AppViewModel {Id = app.Id, Name = app.Name};

                return View(new FormModel<AppViewModel>(model, true));
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppViewModel model)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, model.Id))
            {
                if (!ModelState.IsValid)
                {
                    Alert.Warning();
                    return View(new FormModel<AppViewModel>(model, true));
                }

                var newModel = new AppModel {Id = model.Id, Name = model.Name};

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

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id))
            {
                if (deleteApp.Invoke(id))
                {
                    Alert.Success();
                }
                else
                {
                    Alert.Danger("Something went wrong");
                }
            }
            else
            {
                Alert.Danger("You don't have permission!");
            }

            return RedirectToAction("Index");
        }
    }
}
