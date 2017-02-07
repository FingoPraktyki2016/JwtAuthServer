using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace LegnicaIT.JwtManager.Controllers
{
    [Route("[controller]")]
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
        [HttpGet]
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

            return View(listOfApps);
        }

        [HttpGet("adduser")]
        public ActionResult AddUser(int appId)
        {
            Breadcrumb.Add("Add user", "AddUser", "Application");
            ViewData["appId"] = appId;

            //TODO Adduser View with action AddUser
            return View("AddUser");
        }

        [ValidateAntiForgeryToken]
        [AuthorizeFilter(UserRole.None)] // ???
        [HttpPost("adduser")]
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

            return RedirectToAction("Details", new { id = appuser.AppId });
        }

        [ValidateAntiForgeryToken]
        [HttpPost("deleteuser")]
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

        public List<UserDetailsFromAppViewModel> ListUsers(int appId)
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

            ViewData["appId"] = appId;

            return listOfUsers;
        }

        [HttpGet("changeuserrole")]
        public ActionResult ChangeUserRole(int appId, int userId)
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

        [ValidateAntiForgeryToken]
        [HttpPost("changeuserrole")]
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

            return RedirectToAction("Details", new { id = appId });
        }

        [AuthorizeFilter(UserRole.User)]
        [HttpGet("details")]
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

                var appModel = new AppViewModel {Id = app.Id, Name = app.Name};

                var listUsers = ListUsers(id);
                var combinedModel = new CombinedAppUserDetailsViewModel(appModel) { Users = listUsers };

                return View(combinedModel);
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpGet("Add")]
        public ActionResult Add()
        {
            Breadcrumb.Add("Add application", "Add", "Application");

            var model = new AppViewModel();

            return View(new FormModel<AppViewModel>(model, true));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [ValidateAntiForgeryToken]
        [HttpPost("Add")]
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

        [HttpGet("edit")]
        public ActionResult Edit(int id)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id))
            {
                Breadcrumb.Add("Edit application", "Edit", "Application");

                var app = getApp.Invoke(id);
                var model = new AppViewModel { Id = app.Id, Name = app.Name };

                return View(new FormModel<AppViewModel>(model, true));
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public ActionResult Edit(AppViewModel model)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, model.Id))
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

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("delete")]
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
