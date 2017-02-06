using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IGetUserDetails getUserDetails;

        public AuthController(IOptions<ManagerSettings> managerSettings,
            IGetUserDetails getUserDetails,
            IGetUserApps getUserApps,
            IOptions<LoggerConfig> loggerSettings,
            ISessionService<LoggedUserModel> loggedUserSessionService)
            : base(managerSettings, loggerSettings, getUserApps, loggedUserSessionService)
        {
            this.getUserDetails = getUserDetails;
            Breadcrumb.Add("Authorization", "Index", "Auth");
        }

        [AuthorizeFilter(UserRole.Manager)]
        public ActionResult Index()
        {
            var result = new ResultModel<string>("User have Manager permission");

            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            Breadcrumb.Add("Login", "Login", "Auth");

            var LoginModel = new LoginModel();
            return View(LoginModel);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Email", "Invalid email or password");
                logger.Information("Model is not valid");

                Alert.Warning();

                return View(model);
            }

            var handler = new ApiHelper(Settings.ApiReference);
            var resultString = handler.AcquireToken(model.Email, model.Password, "1");
            var result = JsonConvert.DeserializeObject<ResultModel<object>>(resultString.ResponseMessage);

            if (result.Status.Code == ResultCode.Error)
            {
                logger.Information("Token is not valid");

                return View(model);
            }

            HttpContext.Session.SetString("token", result.Value.ToString());

            var userDetails = getUserDetails.Invoke(model.Email);
            HttpContext.Session.SetString("UserDetails", JsonConvert.SerializeObject(userDetails));

            ViewData["Message"] = model.Email;

            Alert.Success("Logged in");

            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SwitchApp(int appId)
        {
            if (appId == LoggedUser.AppId || appId == 0)
            {
                return View("Error");
            }

            var token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                return View("Error");
            }

            var handler = new ApiHelper(Settings.ApiReference);
            var resultString = handler.SwitchApp(token, appId.ToString());
            var result = JsonConvert.DeserializeObject<ResultModel<object>>(resultString.ResponseMessage);

            if (result.Status.Code == ResultCode.Error)
            {
                logger.Information("Token is not valid");
                return View("Error");
            }

            HttpContext.Session.SetString("token", result.Value.ToString());
            return RedirectToAction("Details", "Application", new { id = appId });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Logout()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                HttpContext.Session.Remove("token");
            }
            else
            {
                logger.Information("Something went wrong during logout");
            }

            HttpContext.Session.Clear();

            Alert.Success("Logged out");

            return RedirectToAction("Login");
        }
    }
}
