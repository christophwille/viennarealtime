using System;
using System.Collections.Generic;

//
// Generated with: http://json2csharp.com/
//
namespace WienerLinien.Api.Realtime.TrafficInfoListProxies
{
    public class TrafficInfoCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public int refTrafficInfoCategoryGroupId { get; set; }
        public string title { get; set; }
    }

    public class TrafficInfoCategoryGroup
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Time
    {
        public string end { get; set; }
        public string resume { get; set; }
        public string start { get; set; }
    }

    public class TrafficInfo
    {
        public string description { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string priority { get; set; }
        public int refTrafficInfoCategoryId { get; set; }
        public List<string> relatedLines { get; set; }
        public List<int> relatedStops { get; set; }
        public Time time { get; set; }
        public string title { get; set; }
    }

    public class Data
    {
        public List<TrafficInfoCategory> trafficInfoCategories { get; set; }
        public List<TrafficInfoCategoryGroup> trafficInfoCategoryGroups { get; set; }
        public List<TrafficInfo> trafficInfos { get; set; }
    }

    public class RootObject
    {
        public Data data { get; set; }
    }
}
