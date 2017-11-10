using System;
using Microsoft.Extensions.Logging;

namespace Axoom.Extensions.Logging
{
    internal static class LogLevelExtensions
    {
        public static NLog.LogLevel AsNlogLogLevel(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.None:
                    return NLog.LogLevel.Off;
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Information:
                    return NLog.LogLevel.Info;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}