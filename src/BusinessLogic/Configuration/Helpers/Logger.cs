using LegnicaIT.JwtAuthServer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace LegnicaIT.BusinessLogic.Configuration.Helpers
{
    public class Logger : IJwtLogger
    {
        public ILogger logger;

        public Logger(Type type, IOptions<LoggerConfig> settings)
        {
            var factory = new LoggerFactory();

            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), settings.Value.Default);
            factory.AddDebug(logLevel);
            var _logger = factory.CreateLogger(type);

            logger = _logger;
        }

        public void Critical(string message)
        {
            logger.LogCritical(message);
        }

        public void Debug(string message)
        {
            logger.LogDebug(message);
        }

        public void Error(string message)
        {
            logger.LogError(message);
        }

        public void Information(string message)
        {
            logger.LogInformation(message);
        }

        public void Trace(string message)
        {
            logger.LogTrace(message);
        }

        public void Warning(string message)
        {
            logger.LogWarning(message);
        }
    }
}
