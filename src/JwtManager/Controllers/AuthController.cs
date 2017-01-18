using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IOptions<ManagerSettings> managerSettings, IOptions<LoggerConfig> loggerSettings)
            : base(managerSettings, loggerSettings)
        {
        }

        [HttpGet("/auth")]
        [AuthorizeFilter(UserRole.SuperAdmin, UserRole.Manager, UserRole.User)]
        public string Index()
        {
            //TO DO don't return clear strings. Do string or var persmission and then return permission.
            //Also use ResultModel as used in JwtAuthServer
            return "user have Permission";
        }

        [HttpGet("/auth/login")]
        public ActionResult Login()
        {
            var LoginModel = new LoginModel();
            return View(LoginModel);
        }

        [HttpPost("/auth/login")]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.Information("Model is not valid");
            }

            var handler = new ApiHelper(Settings.ApiReference);
            var resultString = handler.AcquireToken(model.Email, model.Password, model.AppId);
            var result = JsonConvert.DeserializeObject<ResultModel<object>>(resultString);

            if (result.Status.Code == ResultCode.Error)
            {
                logger.Information("Token is not valid");
              
                return View();
            }
            HttpContext.Session.SetString("token", result.Value.ToString());
            ViewData["Message"] = model.Email;

            return View("LoginSuccess");
        }

        [HttpGet("/auth/logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Clear();
           
            //TODO Change this. Just for test
            return View("Login");
        }
    }
}
