using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Helpers;
using LegnicaIT.JwtManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class BaseController : Controller
    {
        public LoggedUserModel LoggedUser;
        public ManagerSettings Settings { get; }
        public Logger logger { get; set; }
        public AlertHelper Alert = new AlertHelper();

        private readonly IGetUserApps _getUserApps;

        public BaseController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserApps getUserApps)
        {
            Settings = managerSettings.Value;
            logger = new Logger(GetType(), loggerSettings);
            _getUserApps = getUserApps;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserDetails")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                return;
            }

            LoggedUser = new LoggedUserModel(HttpContext.Session.GetString("UserDetails"), HttpContext.Session.GetString("token"));
            ViewData["LoggedUser"] = LoggedUser;

            if (LoggedUser != null)
            {
                ViewData.Add("apps", _getUserApps.Invoke(LoggedUser.UserModel.Id));
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            TempData.Put("alertMessages", Alert.GetAlerts());
            if (!context.ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];

                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        logger.Warning($"Key: {key}, Error: {errorMessage}");
                    }
                }
            }
        }
    }
}