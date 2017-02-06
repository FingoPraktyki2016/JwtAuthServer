using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.User)]
    public class UserController : BaseController
    {
        private readonly IGetUserById getUserById;
        private readonly IEditUser editUser;
        private readonly IEditUserPassword editUserPassword;
        private readonly ICheckUserPermission checkUserPermission;

        public UserController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserById getUserById,
            IEditUser editUser,
            IEditUserPassword editUserPassword,
            ICheckUserPermission checkUserPermission,
            IGetUserApps getUserApps)
            : base(managerSettings, loggerSettings, getUserApps)
        {
            this.getUserById = getUserById;
            this.editUser = editUser;
            this.editUserPassword = editUserPassword;
            this.checkUserPermission = checkUserPermission;

            Breadcrumb.Add("User", "Index", "User");
        }

        public ActionResult Details(int id)
        {
            Breadcrumb.Add("Details", "Details", "User");

            if (id == LoggedUser.UserModel.Id)
            {
                return RedirectToAction("Me");
            }

            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                return View("Error");
            }

            var model = getUserById.Invoke(id);
            var viewModel = new EditUserDetailsViewModel()
            {
                Name = model.Name,
                Email = model.Email
            };

            return View(new FormModel<EditUserDetailsViewModel>(viewModel));
        }

        public ActionResult Me()
        {
            Breadcrumb.Add("Your account", "Me", "User");

            var model = LoggedUser.UserModel;

            var viewModel = new EditUserDetailsViewModel()
            {
                Name = model.Name,
                Email = model.Email
            };

            return View(new FormModel<EditUserDetailsViewModel>(viewModel));
        }

        public ActionResult Edit(int id)
        {
            Breadcrumb.Add("Edit user", "Edit", "User");

            var loggedUser = LoggedUser.UserModel;
            if (loggedUser.Id == id)
            {
                var viewModel = new EditUserDetailsViewModel()
                {
                    Name = loggedUser.Name,
                    Email = loggedUser.Email
                };
                var userViewModel = new FormModel<EditUserDetailsViewModel>(viewModel, true);
                return View(userViewModel);
            }

            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                return View("Error");
            }

            var model = getUserById.Invoke(id);
            var viewModelWrapper = new EditUserDetailsViewModel()
            {
                Name = model.Name,
                Email = model.Email
            };
            var userviewModel = new FormModel<EditUserDetailsViewModel>(viewModelWrapper, true);
            return View(userviewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new FormModel<EditUserDetailsViewModel>(model, true);
                return View(viewModel);
            }
            var userModel = new UserModel() { Id = model.Id, Name = model.Name };
            editUser.Invoke(userModel);

            return RedirectToAction("Details", model.Id);
        }

        public ActionResult ChangePassword()
        {
            Breadcrumb.Add("Change password", "ChangePassword", "User");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new FormModel<EditPasswordViewModel>(model, true);
                return View(viewModel);
            }

            editUserPassword.Invoke(LoggedUser.UserModel.Id, model.NewPassword);
            //Redirect to view "Password changed?"
            return View();
        }
    }
}