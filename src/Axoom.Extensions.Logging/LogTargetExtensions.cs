using System;
using Axoom.Extensions.Logging.Layouts;
using NLog.Layouts;

namespace Axoom.Extensions.Logging
{
    internal static class LogTargetExtensions
    {
        private static readonly Lazy<GelfLayout> _gelfLayout = new Lazy<GelfLayout>(() => new GelfLayout());
        private static readonly Lazy<Layout> _plainLayout = new Lazy<Layout>(() => new PlainLayout());

        public static Layout GetLayout(this LogTarget logTarget)
        {
            switch (logTarget.Format)
            {
                case LogFormat.Gelf:
                    return _gelfLayout.Value;
                case LogFormat.Plain:
                    return _plainLayout.Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logTarget.Format));
            }
        }
    }
}