using System;

namespace MundlTransit.WP8.Services
{
    public static class ApplicationAnalyticsService
    {
        private static IApplicationAnalyticsService _current;

        public static void Initialize(IApplicationAnalyticsService svc)
        {
            _current = svc;
        }

        public static IApplicationAnalyticsService Current
        {
            get
            {
                return _current;
            }
        }
    }
}
