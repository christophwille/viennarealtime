using System;
using System.Collections.Generic;

//
// Generated with: http://json2csharp.com/
//
namespace WienerLinien.Api.Realtime.MonitorProxies
{
public class Geometry
{
    public string type { get; set; }
    public List<double> coordinates { get; set; }
}

public class Attributes
{
    public int rbl { get; set; }
}

public class Properties
{
    public string name { get; set; }
    public string title { get; set; }
    public string municipality { get; set; }
    public int municipalityId { get; set; }
    public string type { get; set; }
    public string coordName { get; set; }
    public Attributes attributes { get; set; }
}

public class LocationStop
{
    public string type { get; set; }
    public Geometry geometry { get; set; }
    public Properties properties { get; set; }
}

public class DepartureTime
{
    public string timePlanned { get; set; }
    public string timeReal { get; set; }
    public int countdown { get; set; }
}

public class Vehicle
{
    public string name { get; set; }
    public string towards { get; set; }
    public string direction { get; set; }
    public string richtungsId { get; set; }
    public bool barrierFree { get; set; }
    public bool realtimeSupported { get; set; }
    public bool trafficjam { get; set; }
    public string type { get; set; }
}

public class Departure
{
    public DepartureTime departureTime { get; set; }
    public Vehicle vehicle { get; set; }
}

public class Departures
{
    public List<Departure> departure { get; set; }
}

public class Line
{
    public string name { get; set; }
    public string towards { get; set; }
    public string direction { get; set; }
    public string richtungsId { get; set; }
    public bool barrierFree { get; set; }
    public bool realtimeSupported { get; set; }
    public bool trafficjam { get; set; }
    public Departures departures { get; set; }
    public string type { get; set; }
    public int lineId { get; set; }
}

public class Monitor
{
    public LocationStop locationStop { get; set; }
    public List<Line> lines { get; set; }
    public List<string> refTrafficInfoNames { get; set; }
}

public class Time
{
    public string start { get; set; }
    public string end { get; set; }
}

public class TrafficInfo
{
    public int refTrafficInfoCategoryId { get; set; }
    public string name { get; set; }
    public string priority { get; set; }
    public string owner { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public Time time { get; set; }
    public List<string> relatedLines { get; set; }
    public List<int> relatedStops { get; set; }
}

public class TrafficInfoCategory
{
    public int id { get; set; }
    public int refTrafficInfoCategoryGroupId { get; set; }
    public string name { get; set; }
    public string trafficInfoNameList { get; set; }
    public string title { get; set; }
}

public class TrafficInfoCategoryGroup
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Data
{
    public List<Monitor> monitors { get; set; }
    public List<TrafficInfo> trafficInfos { get; set; }
    public List<TrafficInfoCategory> trafficInfoCategories { get; set; }
    public List<TrafficInfoCategoryGroup> trafficInfoCategoryGroups { get; set; }
}

public class Message
{
    public string value { get; set; }
    public int messageCode { get; set; }
    public string serverTime { get; set; }
}

public class RootObject
{
    public Data data { get; set; }
    public Message message { get; set; }
}
}