using LegnicaIT.BussinesLogic.Helpers;
using LegnicaIT.JwtManager.Configuration;
using Microsoft.AspNetCore.Mvc;
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
    }
}