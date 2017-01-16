using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
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
        private readonly IGetAppUserRole getAppUserRole;
        private readonly IGetUserId getUserId;

        public AuthController(ICheckUserExist checkUserExist,
            IGetAppUserRole getAppUserRole,
            IGetUserId getUserId,
            IOptions<LoggerConfig> loggerSettings)
            : base(loggerSettings)
        {
            this.checkUserExist = checkUserExist;
            this.getAppUserRole = getAppUserRole;
            this.getUserId = getUserId;
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

            var userId = getUserId.Invoke(model.Email);
            var userRole = getAppUserRole.Invoke(model.AppId, userId);

            var parser = new JwtParser();
            var acquireResult = parser.AcquireToken(model.Email, model.AppId, userRole);
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

            return Json($"logged-user {LoggedUser.Email} logged-user-role: {LoggedUser.Role}");
        }
    }
}