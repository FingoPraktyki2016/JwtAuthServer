using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Models.Token;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models;
using LegnicaIT.JwtAuthServer.Models.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ICheckUserExist checkUserExist;

        public AuthController(ICheckUserExist checkUserExist, IOptions<DebuggerConfig> settings) : base(settings)
        {
            this.checkUserExist = checkUserExist;
        }

        [HttpPost("verify")]
        public JsonResult Verify(VerifyTokenModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();
                logger.Warning("ModelState invalid");

                return Json(errorResult);
            }

            var parser = new JwtParser();
            var verifiyResult = parser.Verify(model.Token);
            var result = new ResultModel<VerifyResultModel>(verifiyResult);

            logger.Information("Action completed");
            return Json(result);
        }

        [HttpPost("acquiretoken")]
        public JsonResult AcquireToken(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();

                logger.Warning("ModelState invalid");
                return Json(errorResult);
            }

            if (!checkUserExist.Invoke(model.Email, model.Password))
            {
                ModelState.AddModelError("Email", "Authentication failed");
                var errorResult = ModelState.GetErrorModel();

                logger.Warning($"AcquireToken: user {model.Email} not found");
                return Json(errorResult);
            }

            // var userId = getUserId.Invoke(model.Email);
            // var userRole = getAppUserRole.Invoke(model.AppId, userId);

            var parser = new JwtParser();
            var acquireResult = parser.AcquireToken(model.Email, model.AppId/*, userRole*/);
            var result = new ResultModel<AcquireTokenModel>(acquireResult);

            logger.Information("Action completed");
            return Json(result);
        }

        [HttpPost("restricted")]
        [Authorize]
        public JsonResult Restricted(RestrictedModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorResult = ModelState.GetErrorModel();

                logger.Warning("ModelState invalid");
                return Json(errorResult);
            }

            logger.Information("Action completed");
            return Json($"logged-user {LoggedUser.Email}");
        }
    }
}
