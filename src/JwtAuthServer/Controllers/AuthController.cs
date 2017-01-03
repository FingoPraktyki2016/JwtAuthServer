using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.Token;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models;
using LegnicaIT.JwtAuthServer.Models.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ICheckUserExist checkUserExist;

        public AuthController(ICheckUserExist checkUserExist)
        {
            this.checkUserExist = checkUserExist;
        }

        [HttpPost("verify")]
        public JsonResult Verify(VerifyTokenModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();
                return Json(errorResult);
            }

            var parser = new JwtParser();
            var verifiyResult = parser.Verify(model.Token);
            var result = new ResultModel<VerifyResultModel>(verifiyResult);

            return Json(result);
        }

        [HttpPost("acquiretoken")]
        public JsonResult AcquireToken(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();
                return Json(errorResult);
            }

            if (!checkUserExist.Invoke(model.Email, model.Password))
            {
                ModelState.AddModelError("Email", "Authentication failed");
                var errorResult = ModelState.GetErrorModel();
                return Json(errorResult);
            }

            var parser = new JwtParser();
            var acquireResult = parser.AcquireToken(model.Email, model.Password, model.AppId);
            var result = new ResultModel<AcquireTokenModel>(acquireResult);

            return Json(result);
        }

        [HttpPost("restricted")]
        [Authorize]
        public JsonResult Restricted(RestrictedModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();
                return Json(errorResult);
            }

            return Json($"logged-user {LoggedUser.Email}");
        }
    }
}