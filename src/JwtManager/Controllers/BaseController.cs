using LegnicaIT.BusinessLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace LegnicaIT.JwtManager.Controllers
{
    public class BaseController : Controller
    {
        public ManagerSettings Settings { get; }
        public Logger logger { get; set; }

        public BaseController(IOptions<ManagerSettings> managerSettings, IOptions<LoggerConfig> loggerSettings)
        {
            Settings = managerSettings.Value;
            logger = new Logger(this.GetType(), loggerSettings);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
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