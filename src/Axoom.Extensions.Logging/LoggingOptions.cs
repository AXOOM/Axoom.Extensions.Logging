using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Axoom.Extensions.Logging
{
    [PublicAPI]
    public class LoggingOptions
    {
        public LoggingOptions()
            : this("AXOOMApp")
        {
        }

        public LoggingOptions(string applicationName)
            : this(applicationName, new List<LogTarget>())
        {
        }

        public LoggingOptions(string applicationName, ICollection<LogTarget> logTargets)
        {
            ApplicationName = applicationName;
            LogTargets = logTargets;
        }

        /// <summary>
        ///  The name of the application.
        /// </summary>
        /// <remarks>
        /// Only used for File log targets as directory and file name. 
        /// </remarks>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// Root directory for storing log files.
        /// </summary>
        /// <remarks>
        /// Only used for File log targets.
        /// </remarks>
        public string RootLogDirectory { get; set; } = Directory.GetCurrentDirectory();
        
        /// <summary>
        /// Used for filtering log message across all registered logger providers.
        /// see also: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?tabs=aspnetcore2x#log-filtering
        /// </summary>
        public Dictionary<string, LogLevel> Filter { get; set; } = new Dictionary<string, LogLevel>();
        
        /// <summary>
        /// Collection of log targets.
        /// </summary>
        public ICollection<LogTarget> LogTargets { get; set; }
    }
}