using System.Reflection;
using Axoom.Extensions.Logging.LayoutRenderers;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.LayoutRenderers;

namespace Axoom.Extensions.Logging
{
    [PublicAPI]
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// Configures the <see cref="ILoggerFactory"/> to use the AXOOM logging configuration.
        /// </summary>
        /// <remarks>Configures the logger factory with a console logtarget and loglevel <c>Debug</c></remarks>
        /// <param name="loggerFactory">The logger factory to configure.</param>
        /// <param name="applicationName">The name of your application.</param>
        public static ILoggerFactory UseAxoomLogging(this ILoggerFactory loggerFactory, string applicationName)
            => loggerFactory.UseAxoomLogging(new DefaultLoggingOptions(applicationName));

        /// <summary>
        /// Configures the <see cref="ILoggerFactory"/> to use the AXOOM logging configuration.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to configure.</param>
        /// <param name="configuration">The logging configuration.</param>
        public static ILoggerFactory UseAxoomLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
            => loggerFactory.UseAxoomLogging(configuration.Get<LoggingOptions>());

        /// <summary>
        /// Configures the <see cref="ILoggerFactory"/> to use the AXOOM logging configuration.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to configure.</param>
        /// <param name="loggingOptions">The logging configuration.</param>
        public static ILoggerFactory UseAxoomLogging(this ILoggerFactory loggerFactory, LoggingOptions loggingOptions)
        {
            LayoutRenderer.Register<SysLogLevelLayoutRenderer>("sysloglevel");
            LayoutRenderer.Register<UnixTimeLayoutRenderer>("unixtime");

            loggerFactory
                .WithFilter(new FilterLoggerSettings {Switches = loggingOptions.Filter})
                .AddNLog();

            LogManager.AddHiddenAssembly(typeof(LoggerFactoryExtensions).GetTypeInfo().Assembly);
            LogManager.Configuration = NlogConfigurationFactory.Create(loggingOptions);
            LogManager.Configuration.Reload();
            LogManager.ReconfigExistingLoggers();

            return loggerFactory;
        }
    }
}