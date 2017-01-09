using LegnicaIT.BusinessLogic.Models.Token;
using LegnicaIT.JwtAuthServer.Helpers;
using LegnicaIT.JwtAuthServer.Models.ResultModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : BaseController
    {

        public VersionController(IOptions<DebuggerConfig> settings) : base(settings)
        {

        }
        
        [HttpGet]
        public JsonResult Version()
        {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            logger.Information("Version: action completed");          
            return Json(version);
        }

        [HttpGet("tester")]
        public string Tester()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            logger.Information("Tester: action completed");
            return environment;
        }
    }
}
