using LegnicaIT.BusinessLogic;
using LegnicaIT.JwtAuthServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LegnicaIT.JwtAuthServer.Controllers
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

            return Json(token);
        }

        [HttpPost("restricted")]
        [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public JsonResult Restricted(RestrictedModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO: error class
                return Json("error");
            }
            var parser = new JwtParser();

            var data = parser.Restricted(model.Data);
            return Json($"restricted-result: {data}");
        }

    }
}
