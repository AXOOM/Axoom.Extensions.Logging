using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Axoom.Extensions.Logging
{
    public class LogLevelExtensionsFacts
    {
        [Fact]
        public void ConvertingMicrosoftLogLevelToNlogLevelWorks()
        {
            LogLevel.None.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Off);
            LogLevel.Trace.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Trace);
            LogLevel.Debug.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Debug);
            LogLevel.Information.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Info);
            LogLevel.Warning.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Warn);
            LogLevel.Error.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Error);
            LogLevel.Critical.AsNlogLogLevel().ShouldBeEquivalentTo(NLog.LogLevel.Fatal);
        }
    }
}