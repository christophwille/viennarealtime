using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Resources;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.Model
{
    public class RoutingTripModel : List<RoutingTripLegModel>
    {
        public static List<RoutingTripModel> TripsToTripModels(List<Trip> trips)
        {
            return trips.Select(t => new RoutingTripModel(t)).ToList();
        }

        public RoutingTripModel(Trip t) : base(t.Legs.Select(l => l.ToModel()))
        {
            Trip = t;
        }

        public Trip Trip { get; set; }

        public string TripName
        {
            get
            {
                return AppResources.Routing_Trip + " " + Trip.Number.ToString();
            }
        }

        public string Duration
        {
            get
            {
                var d = Trip.Duration;
                string ts;

                if (d.Hours == 0)
                {
                    if (d.Seconds == 0)
                    {
                        ts = d.Minutes.ToString() + " " + AppResources.Routing_Minutes;
                    }
                    else
                    {
                        ts = String.Format("{0:mm:ss}", d);
                    }
                }
                else
                {
                    ts = d.ToString("g");
                }

                return String.Format("{0} ({1} - {2})", ts, TripStartTime, TripEndTime);
            }
        }

        public string LegInfo
        {
            get
            {
                string legs = String.Join(", ", this.Select(l => l.DisplayName));
                return String.Format("{0} ({1} {2})", legs, this.Count, AppResources.Routing_Changes);
            }
        }

        public string TripStartTime
        {
            get
            {
                var leg = this.First();
                return leg.Leg.Departure.Time;
            }
        }

        public string TripEndTime
        {
            get
            {
                var leg = this.Last();
                return leg.Leg.Arrival.Time;
            }
        }
    }
}
