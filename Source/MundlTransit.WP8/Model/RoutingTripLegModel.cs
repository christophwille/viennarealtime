using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Resources;
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

        public bool IsWalkingLeg
        {
            get
            {
                return Leg.TypeOfTransportation == RoutingTypeOfTransportation.Walk;
            }
        }

        public bool IsNormalLeg
        {
            get
            {
                return !IsWalkingLeg;
            }
        }

        public string DisplayName
        {
            get
            {
                return !IsWalkingLeg ? Leg.DisplayName : AppResources.Routing_Type_Walk;
            }
        }

        public string DisplayNameWithDirection
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Leg.Direction))
                    return DisplayName;

                return String.Format("{0} ({1})", DisplayName, Leg.Direction);
            }
        }


        public string LegStartTime
        {
            get
            {
                return Leg.Departure.Time;
            }
        }

        public string LegEndTime
        {
            get
            {
                return Leg.Arrival.Time;
            }
        }
    }

    public static class TripLegExtensions
    {
        public static RoutingTripLegModel ToModel(this TripLeg leg)
        {
            return new RoutingTripLegModel(leg);
        }
    }
}
