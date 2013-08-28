using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    [Table("OgdSteige")]
    public class OgdSteig
    {
        // We use the OGD-provided STEIG_ID as primary key
        [PrimaryKey]
        public int Id { get; set; }

        [Indexed(Name = "FKLinienId")]
        public int FK_LinienId { get; set; }
        [Indexed(Name = "FKHaltestellenId")]
        public int FK_HaltestellenId { get; set; }

        public string Richtung { get; set; }
        public int Reihenfolge { get; set; }
        public string RblNummer { get; set; }
        public string Bereich { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Stand { get; set; }
    }
}
