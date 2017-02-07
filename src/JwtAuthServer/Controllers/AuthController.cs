using LegnicaIT.BusinessLogic;
using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Actions.User.Interfaces;
using LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.BusinessLogic.Models.Common;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ICheckUserExist checkUserExist;
        private readonly IGetUserId getUserId;
        private readonly ICheckUserPermissionToApp checkUserPermissionToApp;
        private readonly IGetUserApps getUserApps;

        public AuthController(
            ICheckUserExist checkUserExist,
            IGetUserId getUserId,
            IGetUserApps getUserApps,
            ICheckUserPermissionToApp checkUserPermissionToApp,
            IOptions<LoggerConfig> loggerSettings)
            : base(loggerSettings)
        {
            this.checkUserExist = checkUserExist;
            this.getUserId = getUserId;
            this.checkUserPermissionToApp = checkUserPermissionToApp;
            this.getUserApps = getUserApps;
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

            var userId = getUserId.Invoke(model.Email);
            var appId = getUserApps.Invoke(userId).FirstOrDefault().Id;

            var acquireResult = parser.AcquireToken(model.Email, appId);
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
                ModelState.AddModelError("AppId", "Permission denied");
                var errorResult = ModelState.GetErrorModel();

                return Json(errorResult);
            }

            var parser = new JwtParser();
            var acquireResult = parser.AcquireToken(LoggedUser.Email, appId);
            var result = new ResultModel<string>(acquireResult.Token);

            return Json(result);
        }
    }
}
