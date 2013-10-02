using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    [Table("OgdHaltestellen")]
    public class OgdHaltestelle
    {
        // We use the OGD-provided OBJECTID as primary key
        [PrimaryKey]
        public int Id { get; set; }
        public int Diva { get; set; }

        [Indexed(Name="StationSearchIndex")]
        public string Bezeichnung { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Stand { get; set; }
    }
}
