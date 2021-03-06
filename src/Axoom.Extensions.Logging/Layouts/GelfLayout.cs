using System;
using NLog.Layouts;

namespace Axoom.Extensions.Logging.Layouts
{
    internal class GelfLayout : JsonLayout
    {
        public GelfLayout()
        {
            Attributes.Add(new JsonAttribute("version", "1.1"));
            Attributes.Add(new JsonAttribute("host", Environment.MachineName));
            Attributes.Add(new JsonAttribute("short_message", LayoutFormats.MESSAGE));
            Attributes.Add(new JsonAttribute("full_message", LayoutFormats.MESSAGE_WITH_EXCEPTION));
            Attributes.Add(new JsonAttribute("timestamp", LayoutFormats.TIMESTAMP_UNIX, encode: false));
            Attributes.Add(new JsonAttribute("level", LayoutFormats.SYS_LOGLEVEL, encode: false));
            Attributes.Add(new JsonAttribute("_timestamp", LayoutFormats.TIMESTAMP));
            Attributes.Add(new JsonAttribute("_loglevel", LayoutFormats.LOGLEVEL));
            Attributes.Add(new JsonAttribute("_logger", LayoutFormats.LOGGER));
            Attributes.Add(new JsonAttribute("_callsite", LayoutFormats.CALLSITE));
            Attributes.Add(new JsonAttribute("_exception_type", LayoutFormats.EXCEPTION_TYPE));
            Attributes.Add(new JsonAttribute("_exception_message", LayoutFormats.EXCEPTION_MESSAGE));
            Attributes.Add(new JsonAttribute("_exception_stacktrace", LayoutFormats.EXCEPTION_STACKTRACE, encode: true));
        }
    }
}