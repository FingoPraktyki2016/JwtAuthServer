using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    //to do add appbased authorization

    [AuthorizeFilter(UserRole.User)]
    public class UserController : BaseController
    {
        private readonly IGetUserById getUserById;
        private readonly IEditUser editUser;

        public UserController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserById getUserById,
            IEditUser editUser
            )
            : base(managerSettings, loggerSettings)
        {
            this.getUserById = getUserById;
            this.editUser = editUser;
        }

        public ActionResult Details(int id)
        {
            //if (id == LoggedUser.UserModel.Id)
            //{
            //    RedirectToAction("Me");
            //}

            var model = getUserById.Invoke(id);
            return View(new FormModel<UserModel>(false, model));
        }

        public ActionResult Me()
        {
            var model = LoggedUser.UserModel;
            return View(new FormModel<UserModel>(false, model));
        }

        public ActionResult Edit(int id)
        {
            var model = LoggedUser.UserModel;
            if (model.Id == id)
            {
                var userViewModel = new FormModel<UserModel>(true, model);
                return View(userViewModel);
            }
            var user = getUserById.Invoke(id);
            var viewModel = new FormModel<UserModel>(true, user);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            var viewModel = new FormModel<UserModel>(true, model);
            editUser.Invoke(model);
            return View(viewModel);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserModel model)
        {
            return View();
        }
    }
}