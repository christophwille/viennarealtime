using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.Model
{
    public class RoutingTripLegModel
    {
        public RoutingTripLegModel(TripLeg tl)
        {
            Leg = tl;
        }

        public TripLeg Leg { get; set; }
    }

    public static class TripLegExtensions
    {
        public static RoutingTripLegModel ToModel(this TripLeg leg)
        {
            return new RoutingTripLegModel(leg);
        }
    }
}
