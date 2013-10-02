using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class Trip
    {
        public Trip(TimeSpan duration, bool interchange)
        {
            Duration = duration;
            Interchange = interchange;

            Legs = new List<TripLeg>();
        }

        public TimeSpan Duration { get; set; }
        public bool Interchange { get; set; }
        public List<TripLeg> Legs { get; set; }
    }
}
