using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class Trip
    {
        public Trip(int number, TimeSpan duration, bool interchange)
        {
            Number = number;
            Duration = duration;
            Interchange = interchange;

            Legs = new List<TripLeg>();
        }

        public int Number { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Interchange { get; set; }
        public List<TripLeg> Legs { get; set; }
    }
}
