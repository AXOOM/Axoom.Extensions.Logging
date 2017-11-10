using System;
using System.IO;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace Axoom.Extensions.Logging
{
    internal static class NlogConfigurationFactory
    {
        public static LoggingConfiguration Create(LoggingOptions loggingOptions)
        {
            var nlogConfig = new LoggingConfiguration();
            PopulateNlogTargets(loggingOptions, nlogConfig);
            return nlogConfig;
        }

        private static void PopulateNlogTargets(LoggingOptions loggingOptions, LoggingConfiguration nlogConfig)
        {
            foreach (LogTarget logTarget in loggingOptions.LogTargets)
            {
                Target nlogTarget = CreateNlogTarget(loggingOptions, logTarget);
                nlogConfig.AddTarget(nlogTarget);
                nlogConfig.AddRule(logTarget.Level.AsNlogLogLevel(), NLog.LogLevel.Fatal, nlogTarget);
            }
        }

        private static Target CreateNlogTarget(LoggingOptions loggingOptions, LogTarget logTarget)
        {
            Target nlogTarget;
            switch (logTarget.Type)
            {
                case LogTargetType.File:
                    nlogTarget = CreateFileTarget(loggingOptions, logTarget);
                    break;
                case LogTargetType.Console:
                    nlogTarget = CreateConsoleTarget(logTarget);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logTarget.Type));
            }

            if (logTarget.Async)
            {
                nlogTarget = new AsyncTargetWrapper($"async_{nlogTarget.Name}", nlogTarget);
            }
            return nlogTarget;
        }

        private static ColoredConsoleTarget CreateConsoleTarget(LogTarget logTarget)
            => new ColoredConsoleTarget(name: logTarget.Type.ToString())
            {
                Layout = logTarget.GetLayout()
            };

        private static FileTarget CreateFileTarget(LoggingOptions loggingOptions, LogTarget logTarget)
            => new FileTarget(name: logTarget.Type.ToString())
            {
                FileName = Path.Combine(loggingOptions.RootLogDirectory, loggingOptions.ApplicationName, $"{loggingOptions.ApplicationName}.log"),
                ArchiveFileName = Path.Combine(loggingOptions.RootLogDirectory, loggingOptions.ApplicationName, "archive", $"{loggingOptions.ApplicationName}.zip"),
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
                ArchiveDateFormat = "yyyy-MM-dd",
                MaxArchiveFiles = 30,
                ArchiveAboveSize = 5242880, //5MB
                EnableArchiveFileCompression = true,
                Layout = logTarget.GetLayout()
            };
    }
}