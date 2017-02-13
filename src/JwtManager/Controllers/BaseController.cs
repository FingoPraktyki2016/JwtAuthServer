using LegnicaIT.BusinessLogic.Actions.App.Interfaces;
using LegnicaIT.BusinessLogic.Configuration;
using LegnicaIT.BusinessLogic.Configuration.Helpers;
using LegnicaIT.JwtManager.Configuration;
using LegnicaIT.JwtManager.Helpers;
using LegnicaIT.JwtManager.Models;
using LegnicaIT.JwtManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;

namespace LegnicaIT.JwtManager.Controllers
{
    public class BaseController : Controller
    {
        public LoggedUserModel LoggedUser;
        public ManagerSettings Settings { get; }
        public Logger logger { get; set; }
        public AlertHelper Alert = new AlertHelper();
        protected readonly BreadcrumbHelper Breadcrumb = new BreadcrumbHelper();

        private const string SessionUserDetails = "UserDetails";
        private const string SessionToken = "token";

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

        public string RenderViewToString<T>(string viewName, string masterName, T model, bool customized = false, string controllerName = "")
        {
            using (StringWriter sw = new StringWriter())
            {
                var engine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                ViewEngineResult viewResult =
                    engine.FindView(ControllerContext, viewName, true);
                ViewContext viewContext =
                    new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw, new HtmlHelperOptions());
                viewResult.View.RenderAsync(viewContext);

                return sw.GetStringBuilder().ToString();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionUserDetails)) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString(SessionToken)))
            {
                return;
            }

            LoggedUser = new LoggedUserModel(HttpContext.Session.GetString(SessionUserDetails), HttpContext.Session.GetString(SessionToken));

            if (LoggedUser != null)
            {
                ViewData.Add("apps", _getUserApps.Invoke(LoggedUser.UserModel.Id));
            }

            loggedUserSessionService.AddItem(LoggedUser);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Alert.GetAlerts().Any())
            {
                TempData.Put("alertMessages", Alert.GetAlerts());
            }

            if (Breadcrumb.GetBreadcrumbItems().Any())
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