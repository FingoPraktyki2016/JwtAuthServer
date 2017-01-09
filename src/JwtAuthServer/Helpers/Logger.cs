using LegnicaIT.JwtAuthServer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace LegnicaIT.JwtAuthServer.Helpers
{
    public class Logger : IJwtLogger
    {
        public ILogger logger;

        public Logger(Type type, IOptions<DebuggerConfig> settings)
        {
            var factory = new LoggerFactory();

            var logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), settings.Value.Default);
            factory.AddConsole(includeScopes: true);
            factory.AddDebug(logLevel);
            this.logger = factory.CreateLogger(type);

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
