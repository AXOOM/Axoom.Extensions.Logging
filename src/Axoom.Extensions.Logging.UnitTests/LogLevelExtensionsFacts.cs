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
            LogLevel.None.AsNlogLogLevel().Should().Be(NLog.LogLevel.Off);
            LogLevel.Trace.AsNlogLogLevel().Should().Be(NLog.LogLevel.Trace);
            LogLevel.Debug.AsNlogLogLevel().Should().Be(NLog.LogLevel.Debug);
            LogLevel.Information.AsNlogLogLevel().Should().Be(NLog.LogLevel.Info);
            LogLevel.Warning.AsNlogLogLevel().Should().Be(NLog.LogLevel.Warn);
            LogLevel.Error.AsNlogLogLevel().Should().Be(NLog.LogLevel.Error);
            LogLevel.Critical.AsNlogLogLevel().Should().Be(NLog.LogLevel.Fatal);
        }
    }
}