using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WienerLinien.Api.Routing
{
    public enum WhenOptions
    {
        Departure,
        Arrival
    }

    public enum RouteTypeOption
    {
        LeastTime,
        LeastInterchange,
        LeastWalking
    }

    public class RoutingRequest
    {
        public int FromStation { get; set; }
        public int ToStation { get; set; }
        public DateTime When { get; set; }
        // public WhenOptions WhenOptions { get; set; }
        public RouteTypeOption RouteType { get; set; }
    }
}
