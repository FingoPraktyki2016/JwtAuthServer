using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Helpers;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LegnicaIT.JwtManager.Controllers
{
    public class BaseController : Controller
    {
        public LoggedUserModel LoggedUser;
        public ManagerSettings Settings { get; }
        public Logger logger { get; set; }
        public AlertHelper Alert = new AlertHelper();
        protected readonly BreadcrumbHelper Breadcrumb = new BreadcrumbHelper();

        private readonly IGetUserApps _getUserApps;
        private readonly ISessionService<LoggedUserModel> loggedUserSessionService;

        public BaseController(
            IOptions<ManagerSettings> managerSettings,
            IOptions<LoggerConfig> loggerSettings,
            IGetUserApps getUserApps,
            ISessionService<LoggedUserModel> loggedUserSessionService)
        {
            Settings = managerSettings.Value;
            logger = new Logger(GetType(), loggerSettings);
            _getUserApps = getUserApps;
            this.loggedUserSessionService = loggedUserSessionService;
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

            if (LoggedUser != null)
            {
                ViewData.Add("apps", _getUserApps.Invoke(LoggedUser.UserModel.Id));
            }

            loggedUserSessionService.AddItem(LoggedUser);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Alert.GetAlerts().Count > 0)
            {
                // FIXME: Display doesn't work
                TempData.Put("alertMessages", Alert.GetAlerts());
            }

            if (Breadcrumb.GetBreadcrumbItems().Count > 0)
            {
                ViewData.Add("breadcrumbItems", Breadcrumb.GetBreadcrumbItems());
            }

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