using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing.RoutingProxies
{
    public class Parameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Frequency
    {
        public string avDuration { get; set; }
        public string avTimeGap { get; set; }
        public string hasFrequency { get; set; }
        public string maxDuration { get; set; }
        public string maxTimeGap { get; set; }
        public string minDuration { get; set; }
        public string minTimeGap { get; set; }
        public List<object> modes { get; set; }
        public string tripIndex { get; set; }
    }

    public class Diva
    {
        public string branch { get; set; }
        public string dir { get; set; }
        public string line { get; set; }
        public string network { get; set; }
        public string opCode { get; set; }
        public string @operator { get; set; }
        public string project { get; set; }
        public string stateless { get; set; }
        public string supplement { get; set; }
    }

    public class Mode
    {
        public string code { get; set; }
        public string desc { get; set; }
        public string destID { get; set; }
        public string destination { get; set; }
        public Diva diva { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string type { get; set; }
    }

    public class DateTime
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Ref
    {
        public string NaPTANID { get; set; }
        public string area { get; set; }
        public List<object> attrs { get; set; }
        public string coords { get; set; }
        public string id { get; set; }
        public string platform { get; set; }
    }

    public class Stamp
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Point
    {
        public DateTime dateTime { get; set; }
        public string desc { get; set; }
        public List<Link> links { get; set; }
        public string name { get; set; }
        public string nameWithPlace { get; set; }
        public string omc { get; set; }
        public string place { get; set; }
        public string placeID { get; set; }
        public Ref @ref { get; set; }
        public Stamp stamp { get; set; }
        public string usage { get; set; }
    }

    public class Ref2
    {
        public string NaPTANID { get; set; }
        public string area { get; set; }
        public List<object> attrs { get; set; }
        public string coords { get; set; }
        public string depDateTime { get; set; }
        public string id { get; set; }
        public string platform { get; set; }
        public string arrDateTime { get; set; }
    }

    public class StopSeq
    {
        public string name { get; set; }
        public string nameWO { get; set; }
        public string nameWithPlace { get; set; }
        public string omc { get; set; }
        public string place { get; set; }
        public string placeID { get; set; }
        public string platformName { get; set; }
        public Ref2 @ref { get; set; }
    }

    public class Leg
    {
        public Frequency frequency { get; set; }
        public Mode mode { get; set; }
        public string path { get; set; }
        public List<Point> points { get; set; }
        public List<StopSeq> stopSeq { get; set; }
    }

    public class Trip2
    {
        public List<object> attrs { get; set; }
        public string desc { get; set; }
        public string duration { get; set; }
        public string interchange { get; set; }
        public List<Leg> legs { get; set; }
    }

    public class Trip
    {
        public Trip2 trip { get; set; }
    }

    public class RootObject
    {
        public List<Parameter> parameters { get; set; }
        public List<Trip> trips { get; set; }
    }
}
