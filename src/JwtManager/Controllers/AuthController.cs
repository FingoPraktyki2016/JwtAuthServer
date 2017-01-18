using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IOptions<ManagerSettings> managerSettings, IOptions<LoggerConfig> loggerSettings)
          : base(managerSettings, loggerSettings)
        {
        }

        [HttpGet("/auth")]
        [AuthorizeFilter(UserRole.One, UserRole.Two)]
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
            var handler = new ApiHelper(Settings.ApiReference);

                if (!ModelState.IsValid)
            {
                logger.Information("Model is not valid");
            }

            var resultToken = handler.AcquireToken(model.Email, model.Password, model.AppId);
            var resultVerify = handler.Verify(resultToken);
            bool isValid = bool.Parse(resultVerify);

            if (!isValid)
            {
                logger.Information("Token is not valid");
                return View();
            }
            
            HttpContext.Session.SetString("token", resultToken);

      //      logger.Information("Something went wrong. Redisplaying view");

            return View();
        }
    }
}