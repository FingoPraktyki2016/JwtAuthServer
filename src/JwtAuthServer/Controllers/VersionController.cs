using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : BaseController
    {
        [HttpGet]
        public JsonResult Version()
        {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();
            return Json(version);
        }

        [HttpGet("tester")]
        public string Tester()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            logger.Information("Info");
            logger.Debug("Debug");
            logger.Warning("Warning");
            logger.Error("Error");
            logger.Trace("Trace");
            logger.Critical("Critical");

            return environment;
        }
    }
}
