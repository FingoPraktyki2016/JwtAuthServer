using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
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

        public BaseController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings)
        {
            Settings = managerSettings.Value;
            logger = new Logger(GetType(), loggerSettings);
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
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.ModelState.IsValid)
            {
                return;
            }

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