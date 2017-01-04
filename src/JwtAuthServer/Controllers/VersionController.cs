using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace LegnicaIT.JwtAuthServer.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : BaseController
    {
        public VersionController(ILogger<VersionController> logger) : base(logger)
        {
        }

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

            _logger.LogDebug("Environment: {0}", environment);

            _logger.LogCritical("Log Critical");
            _logger.LogDebug("Log Debug");
            _logger.LogError("Log Error");
            _logger.LogInformation("Log Information");
            _logger.LogTrace("Log Trace");
            _logger.LogWarning("Log Warning");

            return environment;
        }
    }
}
