using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MundlTransit.WP8.Services
{
    public class DebugApplicationAnalyticsService : IApplicationAnalyticsService
    {
        public void LogEvent(string eventName)
        {
            Debug.WriteLine("AppAnalyticsEvent: " + eventName);
        }

        public void LogEvent(string eventName, IDictionary<string, object> properties)
        {
            Debug.WriteLine("AppAnalytisEvent: " + eventName + " - " + String.Join(", ", properties.Keys));
        }

        public void LogPageView(string pagePath)
        {
            Debug.WriteLine("AppAnalytisPageView: " + pagePath);
        }
    }
}
