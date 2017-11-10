using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Axoom.Extensions.Logging
{
    [PublicAPI]
    public class LogTarget
    {
        public LogTarget()
        {
        }

        public LogTarget(LogTargetType type, LogLevel level)
            : this(type, level, LogFormat.Gelf)
        {
            Type = type;
            Level = level;
        }

        public LogTarget(LogTargetType type, LogLevel level, LogFormat logFormat)
        {
            Type = type;
            Level = level;
            Format = logFormat;
        }

        public LogTargetType Type { get; set; }
        public LogLevel Level { get; set; }
        public LogFormat Format { get; set; } = LogFormat.Gelf;

        /// <summary>
        /// Indicating whether all log messages will be written on a separate thread so your main thread can be unblocked more quickly. 
        /// Asynchronous logging is recommended for multi-threaded server applications which run for a long time and is not recommended 
        /// for quickly-finishing command line applications.
        /// </summary>
        /// <remarks>Default: true</remarks>
        public bool Async { get; set; } = true;
    }
}