using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.Model
{
    public class RoutingTripGroupModel
    {
        public static List<RoutingTripGroupModel> TripsToTripGroupModels(List<Trip> trips)
        {
            var groups = new Dictionary<int, RoutingTripGroupModel>();

            foreach (var trip in trips)
            {
                var t = new RoutingTripModel(trip);
                
                int groupId = t.Trip.TripGroupId;
                RoutingTripGroupModel currentGroup = null;

                if (groups.ContainsKey(groupId))
                {
                    currentGroup = groups[groupId];
                }
                else
                {
                    currentGroup = new RoutingTripGroupModel()
                    {
                        Trips = new List<RoutingTripModel>(),
                        DefaultTrip = t
                    };

                    groups.Add(groupId, currentGroup);
                }

                currentGroup.Trips.Add(t);
            }

            return groups.Values.ToList();
        }

        public List<RoutingTripModel> Trips { get; set; }
        public RoutingTripModel DefaultTrip { get; set; }
    }
}
