using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Enums;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models;
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
        private readonly ICheckUserPermissionToApp checkUserPermissionToApp;

        public AuthController(
            ICheckUserExist checkUserExist,
            IGetAppUserRole getAppUserRole,
            IGetUserId getUserId,
            ICheckUserPermissionToApp checkUserPermissionToApp,
            IOptions<LoggerConfig> loggerSettings)
            : base(loggerSettings)
        {
            this.checkUserExist = checkUserExist;
            this.getAppUserRole = getAppUserRole;
            this.getUserId = getUserId;
            this.checkUserPermissionToApp = checkUserPermissionToApp;
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
            var result = new ResultModel<bool>(verifiyResult.IsValid);

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
            var acquireResult = parser.AcquireToken(model.Email, 1, UserRole.User);
            var result = new ResultModel<string>(acquireResult.Token);

            return Json(result);
        }

        [HttpPost("switchapp")]
        [Authorize]
        public JsonResult SwitchApp(int appId)
        {
            var userId = getUserId.Invoke(LoggedUser.Email);
            if (!checkUserPermissionToApp.Invoke(userId, appId))
            {
                ModelState.AddModelError("AppId", "User dones't have permission to this app");
                var errorResult = ModelState.GetErrorModel();

                return Json(errorResult);
            }
            var userRole = getAppUserRole.Invoke(appId, userId);

            var parser = new JwtParser();
            var acquireResult = parser.AcquireToken(LoggedUser.Email, appId, userRole);
            var result = new ResultModel<string>(acquireResult.Token);

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
