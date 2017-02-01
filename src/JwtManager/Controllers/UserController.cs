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

        public UserController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserById getUserById,
            IEditUser editUser,
            IEditUserPassword editUserPassword
            )
            : base(managerSettings, loggerSettings)
        {
            this.getUserById = getUserById;
            this.editUser = editUser;
            this.editUserPassword = editUserPassword;
        }

        [AuthorizeFilter(UserRole.User)]
        public ActionResult Details(int id)
        {
            if (id == LoggedUser.UserModel.Id)
            {
                RedirectToAction("Me", LoggedUser.UserModel);
            }

            //TODO: Add app validation/role validation

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

            //TODO: Add appid and role validation
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
            if (ModelState.IsValid)
            {
                var userModel = new UserModel() { Id = model.Id, Name = model.Name };
                editUser.Invoke(userModel);

                return RedirectToAction("Details", model.Id);
            }

            var viewModel = new FormModel<EditUserDetailsViewModel>(model, true);
            return View(viewModel);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(EditPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                editUserPassword.Invoke(LoggedUser.UserModel.Id, model.NewPassword);
            }
            //Redirect to view "Password changed?"
            return View();
        }
    }
}