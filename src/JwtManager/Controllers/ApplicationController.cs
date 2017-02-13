using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Models.User;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace LegnicaIT.JwtManager.Controllers
{
    [Route("[controller]")]
    [AuthorizeFilter(UserRole.User)]
    public class ApplicationController : BaseController
    {
        private readonly IGetAllUsers getAllUsers;
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
        private readonly ICheckUserPermissionToApp checkUserPermissionToApp;
        private readonly ICheckUserPermission checkUserPermission;

        public ApplicationController(
            IGetAllUsers getAllUsers,
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
            ICheckUserPermissionToApp checkUserPermissionToApp,
            ICheckUserPermission checkUserPermission,
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
            this.checkUserPermissionToApp = checkUserPermissionToApp;
            this.checkUserPermission = checkUserPermission;
            this.getAllUsers = getAllUsers;

            Breadcrumb.Add("Application", "Index", "Application");
        }

        [AuthorizeFilter(UserRole.User)]
        [HttpGet]
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

            return View(listOfApps);
        }

        [HttpGet("adduser")]
        [AuthorizeFilter(UserRole.Manager)]
        public IActionResult AddUser(int appId)
        {
            Breadcrumb.Add("Add user", "AddUser", "Application");
            ViewData["appId"] = appId;

            return View("AddUser");
        }

        [ValidateAntiForgeryToken]
        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("adduser")]
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

            return RedirectToAction("Details", new { id = appuser.AppId });
        }

        [ValidateAntiForgeryToken]
        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("deleteuser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                Alert.Danger("You don't have permission");
                return RedirectToAction("Details", new { id = LoggedUser.AppId });
            }

            if (deleteUserApp.Invoke(id, LoggedUser.AppId))
            {
                Alert.Success();
                return RedirectToAction("Details", new { id = LoggedUser.AppId });
            }
            else
            {
                Alert.Danger("Something went wrong");
            }
            return RedirectToAction("Details", new { id = LoggedUser.AppId });
        }

        [ValidateAntiForgeryToken]
        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("deleteuser/{id}")]
        public ActionResult UserDeleteAll(int id)
        {
            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                Alert.Danger("You don't have permission");
                return RedirectToAction("Details", new { id = LoggedUser.AppId });
            }

            if (deleteUserApp.Invoke(id, LoggedUser.AppId))
            {
                Alert.Success();
                return RedirectToAction("Details", new { id = LoggedUser.AppId });
            }
            else
            {
                Alert.Danger("Something went wrong");
            }
            return RedirectToAction("Details", new { id = LoggedUser.AppId });
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

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpGet("alluserslist")]
        public IActionResult AllUsersList()
        {
            var usersList = getAllUsers.Invoke();

            var userModelList = new List<UserSimpleModel>();

            foreach (var user in usersList)
            {
                var model = new UserSimpleModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name
                };

                userModelList.Add(model);
            }

            return View(userModelList);
        }

        [AuthorizeFilter(UserRole.Manager)]
        [HttpGet("changeuserrole")]
        public IActionResult ChangeUserRole(int appId, int userId)
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
        [AuthorizeFilter(UserRole.Manager)]
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
        public IActionResult Details(int id)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id))
            {
                Breadcrumb.Add("Application details", "Details", "Application");

                var app = getApp.Invoke(id);

                if (app == null)
                {
                    return View("Error");
                }

                var appModel = new AppViewModel { Id = app.Id, Name = app.Name };

                var listUsers = ListUsers(id);
                var combinedModel = new CombinedAppUserDetailsViewModel(appModel) { Users = listUsers };

                return View(combinedModel);
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [HttpGet("Add")]
        public IActionResult Add()
        {
            Breadcrumb.Add("Add application", "Add", "Application");

            var model = new AppViewModel();

            return View(new FormModel<AppViewModel>(model, true));
        }

        [AuthorizeFilter(UserRole.SuperAdmin)]
        [ValidateAntiForgeryToken]
        [HttpPost("Add")]
        public IActionResult Add(AppViewModel model)
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

        [AuthorizeFilter(UserRole.Manager)]
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id, ActionType.Edit))
            {
                Breadcrumb.Add("Edit application", "Edit", "Application");

                var app = getApp.Invoke(id);
                var model = new AppViewModel { Id = app.Id, Name = app.Name };

                return View(new FormModel<AppViewModel>(model, true));
            }

            Alert.Danger("You don't have permission!");
            return RedirectToAction("Index");
        }

        [AuthorizeFilter(UserRole.Manager)]
        [ValidateAntiForgeryToken]
        [HttpPost("edit")]
        public IActionResult Edit(AppViewModel model)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, model.Id, ActionType.Edit))
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
        [AuthorizeFilter(UserRole.Manager)]
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (checkUserPermissionToApp.Invoke(LoggedUser.UserModel.Id, id, ActionType.Delete))
            {
                if (deleteApp.Invoke(id))
                {
                    Alert.Success();
                }
                else
                {
                    Alert.Danger("Something went wrong");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Alert.Danger("You don't have permission!");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
