using System;
using System.Linq;
using NLog.Layouts;

namespace Axoom.Extensions.Logging
{
    internal static class JsonLayoutExtensions
    {
        public static string GetAttributeLayout(this JsonLayout jsonLayout, string attributeName)
        {
            var layout = (SimpleLayout) jsonLayout.Attributes.Single(attr => attr.Name.Equals(attributeName, StringComparison.OrdinalIgnoreCase)).Layout;
            return layout.OriginalText;
        }
    }
}