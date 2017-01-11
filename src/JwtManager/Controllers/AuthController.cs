using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Authorization;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace LegnicaIT.JwtManager.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IOptions<ManagerSettings> settings)
            : base(settings)
        {

        }

        [HttpGet("/auth")]
        [AuthorizeFilter(UserRole.One, UserRole.Two)]
        public string Index()
        {
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
            
            var resultToken = handler. AcquireToken(model.Email,model.Password,model.AppId);
            var resultVerify = handler.Verify(resultToken);

            //TODO Sprawdzić czy poprawnie przeszedl verify, jesli tak to  zachowac token w sesji 
            HttpContext.Session.SetString("token", resultToken);

            return View();
        }

    }
}
