using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class Trip
    {
        public Trip(int tripGroupId, int number, TimeSpan duration, int interchanges)
        {
            TripGroupId = tripGroupId;
            Number = number;
            Duration = duration;
            Interchanges = interchanges;

            Legs = new List<TripLeg>();
        }

        public int TripGroupId { get; set; }
        public int Number { get; set; }
        public TimeSpan Duration { get; set; }
        public int Interchanges { get; set; }
        public List<TripLeg> Legs { get; set; }
    }
}
