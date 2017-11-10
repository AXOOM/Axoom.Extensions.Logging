using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using NLog;
using Xunit;

namespace Axoom.Extensions.Logging
{
    public class AxoomLogManagerFacts
    {
        private readonly FieldInfo _hiddenAssembliesField;
        private IEnumerable<Assembly> HiddenAssemblies => (HashSet<Assembly>) _hiddenAssembliesField.GetValue(null);

        public AxoomLogManagerFacts()
            => _hiddenAssembliesField = typeof(LogManager).GetField("_hiddenAssemblies", BindingFlags.NonPublic | BindingFlags.Static);

        [Fact]
        public void AddingHiddenAssemblyAddsToLogManager()
        {
            Assembly expectedAssembly = typeof(AxoomLogManagerFacts).GetTypeInfo().Assembly;

            AxoomLogManager.AddHiddenAssembly(expectedAssembly);

            HiddenAssemblies.Should().Contain(expectedAssembly);
        }
    }
}