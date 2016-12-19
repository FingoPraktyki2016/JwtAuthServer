using JwtAuthServer.Models;
using LegnicaIT.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("verify")]
        public JsonResult Verify(VerifyTokenModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: error class
                return Json("error");
            }

            var parser = new JwtParser();
            var result = parser.Verify(model.Token);

            return Json(result);
        }

        [HttpPost("acquiretoken")]
        public JsonResult AcquireToken(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: error class
                return Json("error");
            }

            var parser = new JwtParser();
            var token = parser.AcquireToken(model.Email, model.Password, model.AppId);

            return Json($"acquireToken-result: {token}");
        }
    }
}
