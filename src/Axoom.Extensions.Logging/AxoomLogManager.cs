using System.Reflection;
using JetBrains.Annotations;
using NLog;

namespace Axoom.Extensions.Logging
{
    [PublicAPI]
    public static class AxoomLogManager
    {
        /// <summary>
        /// Adds the given assembly which will be skipped when the logging framework is trying to find the calling method on stack trace.
        /// </summary>
        public static void AddHiddenAssembly(Assembly assembly) => LogManager.AddHiddenAssembly(assembly);
    }
}