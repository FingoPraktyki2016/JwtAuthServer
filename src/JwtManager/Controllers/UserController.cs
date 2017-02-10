using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Models.User;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    [Route("user")]
    [AuthorizeFilter(UserRole.User)]
    public class UserController : BaseController
    {
        private readonly IGetUserById getUserById;
        private readonly IEditUser editUser;
        private readonly IEditUserPassword editUserPassword;
        private readonly ICheckUserPermission checkUserPermission;
        private readonly IGetUserDetails getUserDetails;

        public UserController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserById getUserById,
            IEditUser editUser,
            IEditUserPassword editUserPassword,
            ICheckUserPermission checkUserPermission,
            IGetUserApps getUserApps,
            IGetUserDetails getUserDetails,
            ISessionService<LoggedUserModel> loggedUserSessionService)
            : base(managerSettings, loggerSettings, getUserApps, loggedUserSessionService)
        {
            this.getUserById = getUserById;
            this.editUser = editUser;
            this.editUserPassword = editUserPassword;
            this.checkUserPermission = checkUserPermission;
            this.getUserDetails = getUserDetails;
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            Breadcrumb.Add("User details", "Details", "User");

            if (id == LoggedUser.UserModel.Id)
            {
                return RedirectToAction("Me");
            }

            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                Alert.Danger("You're not allowed to see this page");
                return View("Error");
            }

            var model = getUserById.Invoke(id);
            var viewModel = new EditUserDetailsViewModel
            {
                Name = model.Name,
                Email = model.Email
            };

            return View(new FormModel<EditUserDetailsViewModel>(viewModel));
        }

        [HttpGet("me")]
        public IActionResult Me()
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

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Breadcrumb.Add("Edit user", "Edit", "User");

            if (!checkUserPermission.Invoke(LoggedUser.UserModel.Id, LoggedUser.AppId, id))
            {
                Alert.Danger("You're not allowed to see this page");
                return View("Error");
            }

            var model = (LoggedUser.UserModel.Id == id) ? LoggedUser.UserModel : getUserById.Invoke(id);

            var viewModelWrapper = new EditUserDetailsViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email
            };
            var userviewModel = new FormModel<EditUserDetailsViewModel>(viewModelWrapper, true);
            return View(userviewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
                var viewModel = new FormModel<EditUserDetailsViewModel>(model, true);
                return View(viewModel);
            }
            var userModel = new UserModel { Id = model.Id, Name = model.Name };
            editUser.Invoke(userModel);
            if (model.Id == LoggedUser.UserModel.Id)
            {
                var userDetails = getUserDetails.Invoke(LoggedUser.UserModel.Email);
                HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(userDetails));
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet("changepassword")]
        public IActionResult ChangePassword()
        {
            Breadcrumb.Add("Change your password", "ChangePassword", "User");

            return View();
        }

        [HttpPost("changepassword")]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Alert.Warning();
                var viewModel = new FormModel<EditPasswordViewModel>(model, true);
                return View(viewModel);
            }

            editUserPassword.Invoke(LoggedUser.UserModel.Id, model.NewPassword);

            Alert.Success("Your password has been changed");

            return RedirectToAction("Me");
        }
    }
}