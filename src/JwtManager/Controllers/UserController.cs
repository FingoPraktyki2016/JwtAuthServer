using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    [AuthorizeFilter(UserRole.User)]
    public class UserController : BaseController
    {
        public UserController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
        }

        /*
          * GET: /User/Details
         */

        [AuthorizeFilter(UserRole.Manager)]
        public IActionResult Details(int id)
        {
            var model = new UserModel();
            return View(model);
        }

        /*
         * GET: /User/Me
         */

        public IActionResult Me()
        {
            var model = LoggedUser.GetModel();
            return View(new FormModel<UserModel>(false, model));
        }

        /*
         * GET: /User/Edit
         */

        public IActionResult Edit()
        {
            var model = LoggedUser.GetModel();
            var viewModel = new FormModel<UserModel>(true, model);
            return View(viewModel);
        }

        /*
         * POST: /User/Edit
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel model)
        {
            var viewModel = new FormModel<UserModel>(true, model);
            return View(viewModel);
        }

        /*
         * GET: /User/ChangePassword
         */

        public IActionResult ChangePassword()
        {
            return View();
        }

        /*
         * POST: /User/ChangePassword
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(UserModel model)
        {
            return View();
        }
    }
}
