using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using LegnicaIT.BussinesLogic.Helpers;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : BaseController
    {
        public VersionController(IOptions<LoggerConfig> settings) : base(settings)
        {
        }

        [HttpGet]
        public JsonResult Version()
        {
            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            logger.Information("Action completed");
            return Json(version);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("tester")]
        public string Tester()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            logger.Information("Action completed");
            return environment;
        }
    }
}