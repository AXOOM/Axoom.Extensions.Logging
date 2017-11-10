using Microsoft.Extensions.Logging;

namespace Axoom.Extensions.Logging
{
    internal class DefaultLoggingOptions : LoggingOptions
    {
        public DefaultLoggingOptions(string applicationName)
            : base(applicationName, new[] {new LogTarget(LogTargetType.Console, LogLevel.Debug)})
        {
        }
    }
}