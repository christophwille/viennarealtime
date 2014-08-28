using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MundlTransit.WP8.Model
{
    public class Wgs84Location
    {
        public Wgs84Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public Wgs84Location(Geocoordinate gc)
        {
            Longitude = gc.Point.Position.Longitude;
            Latitude = gc.Point.Position.Latitude;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
