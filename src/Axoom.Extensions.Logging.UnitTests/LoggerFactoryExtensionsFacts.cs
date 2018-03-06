using System;
using System.Collections.Generic;
using System.Linq;
using Axoom.Extensions.Logging.LayoutRenderers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xunit;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Axoom.Extensions.Logging
{
    public class LoggerFactoryExtensionsFacts
    {
        private const string APPLICATION_NAME = "unittest";
        private readonly LoggerFactory _loggerFactory;

        public LoggerFactoryExtensionsFacts()
        {
            _loggerFactory = new LoggerFactory();
        }

        [Fact]
        public void AddingAxoomLoggingWithoutLoggingOptionsAddsLoggingWithConsoleTarget()
        {
            _loggerFactory.UseAxoomLogging(APPLICATION_NAME);

            LogManager.Configuration.AllTargets.OfType<ColoredConsoleTarget>().Should().NotBeEmpty();
        }

        [Fact]
        public void AddingAxoomLoggingFromConfigurationIsWorking()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"applicationName", APPLICATION_NAME},
                    {"logTargets:0:type", "Console"},
                    {"logTargets:0:level", "Debug"},
                    {"logTargets:0:format", "Plain"}
                })
                .Build();

            _loggerFactory.UseAxoomLogging(configuration);

            LogManager.Configuration.AllTargets.OfType<ColoredConsoleTarget>().Should().NotBeEmpty();
        }

        [Fact]
        public void AddingAxoomLoggingFromLoggingOptionsIsWorking()
        {
            var loggingOptions = new LoggingOptions(APPLICATION_NAME, new[] {new LogTarget(LogTargetType.Console, LogLevel.Debug)});

            _loggerFactory.UseAxoomLogging(loggingOptions);

            LogManager.Configuration.AllTargets.OfType<ColoredConsoleTarget>().Should().NotBeEmpty();
        }

        [Fact]
        public void AddingAxoomLoggingRegistersSysLogLevelLayoutRenderer()
        {
            var loggingOptions = new LoggingOptions(APPLICATION_NAME, new[] {new LogTarget(LogTargetType.Console, LogLevel.Debug)});

            _loggerFactory.UseAxoomLogging(loggingOptions);

            Type type;
            ConfigurationItemFactory.Default.LayoutRenderers.TryGetDefinition("sysloglevel", out type);
            type.Should().Be(typeof(SysLogLevelLayoutRenderer));
        }
        
        [Fact]
        public void AddingAxoomLoggingRegistersUnixTimeLayoutRenderer()
        {
            var loggingOptions = new LoggingOptions(APPLICATION_NAME, new[] {new LogTarget(LogTargetType.Console, LogLevel.Debug)});

            _loggerFactory.UseAxoomLogging(loggingOptions);

            Type type;
            ConfigurationItemFactory.Default.LayoutRenderers.TryGetDefinition("unixtime", out type);
            type.Should().Be(typeof(UnixTimeLayoutRenderer));
        }
    }
}