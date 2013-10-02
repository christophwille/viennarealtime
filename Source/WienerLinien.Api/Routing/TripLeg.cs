using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class TripLeg
    {
        public TripLeg(RoutingTypeOfTransportation tt, string name, string direction)
        {
            TypeOfTransportation = tt;
            DisplayName = name;
            Direction = direction;
        }

        public RoutingTypeOfTransportation TypeOfTransportation { get; set; }
        public string DisplayName { get; set; }
        public string Direction { get; set; }

        public LegPoint Departure { get; set; }
        public LegPoint Arrival { get; set; }
    }
}
