using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Model
{
    public class MapHaltestelleModel
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public GeoCoordinate GeoCoordinate { get; set; }
    }
}
