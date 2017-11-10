using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using Xunit;

namespace Axoom.Extensions.Logging
{
    public class NlogConfigurationFactoryFacts
    {
        private readonly LoggingOptions _loggingOptions;

        public NlogConfigurationFactoryFacts() => _loggingOptions = new LoggingOptions("UnitTest");

        [Fact]
        public void ConfiguredFileTargetIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.File, LogLevel.Trace));
            
            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            var target = nlogConfig.AllTargets.FirstOrDefault(t => t.Name.Equals("async_File"));
            target.Should().NotBeNull();
        }

        [Fact]
        public void ConfiguredConsoleTargetIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Trace));
            
            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            var target = nlogConfig.AllTargets.FirstOrDefault(t => t.Name.Equals("async_Console"));
            target.Should().NotBeNull();
        }
        
          [Fact]
        public void ConfiguredAsyncTargetIsAddedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Information) {Async = true});

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            Target target = nlogConfig.AllTargets.FirstOrDefault();
            target.Should().NotBeNull();
            target.Name.Should().Be($"async_{LogTargetType.Console}");
            var asyncTarget = (AsyncTargetWrapper) target;
            asyncTarget.WrappedTarget.Name.Should().Be(LogTargetType.Console.ToString());
        }

        [Fact]
        public void ConfiguredOffLogLevelIsPromotedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.None));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.Should().BeEmpty();
        }

        [Fact]
        public void ConfiguredTraceLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Trace));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Trace);
        }

        [Fact]
        public void ConfiguredDebugLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Debug));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Debug);
        }

        [Fact]
        public void ConfiguredInfoLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Information));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Info);
        }

        [Fact]
        public void ConfiguredWarnLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Warning));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Warn);
        }

        [Fact]
        public void ConfiguredErrorLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Error));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Error);
        }

        [Fact]
        public void ConfiguredFatalLogLevelIsPopulatedToNlog()
        {
            _loggingOptions.LogTargets.Add(new LogTarget(LogTargetType.Console, LogLevel.Critical));

            LoggingConfiguration nlogConfig = NlogConfigurationFactory.Create(_loggingOptions);

            LoggingRule loggingRule = nlogConfig.LoggingRules[0];
            loggingRule.Should().NotBeNull();
            loggingRule.Levels.FirstOrDefault().Should().Be(NLog.LogLevel.Fatal);
        }
    }
}