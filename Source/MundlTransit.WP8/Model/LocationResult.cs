using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MundlTransit.WP8.Model
{
    public class LocationResult
    {
        public LocationResult(Geoposition pos)
        {
            Position = pos;
        }

        public LocationResult(string errMsg)
        {
            ErrorMessage = errMsg;
        }

        public Geoposition Position { get; set; }

        public bool Succeeded
        {
            get { return null != Position; }
        }

        public string ErrorMessage { get; set; }
    }
}
