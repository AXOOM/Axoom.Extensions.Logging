using FluentAssertions;
using NLog.Layouts;
using Xunit;

namespace Axoom.Extensions.Logging
{
    public class JsonLayoutExtensionsFacts
    {
        [Fact]
        public void GettingAttributeLayoutReturnsOriginalLayoutText()
        {
            const string originalText = "${message}";
            var jsonLayout = new JsonLayout();
            jsonLayout.Attributes.Add(new JsonAttribute("property", originalText));

            string attributeLayout = jsonLayout.GetAttributeLayout("property");

            attributeLayout.Should().Be(originalText);
        }
    }
}