using System;
using System.Collections.Generic;

namespace MundlTransit.WP8.Services
{
    // See VSO Application Insights for interface definition
    public interface IApplicationAnalyticsService
    {
        void LogEvent(string eventName);
        void LogEvent(string eventName, IDictionary<string, object> properties);
        void LogPageView(string pagePath);
    }
}
