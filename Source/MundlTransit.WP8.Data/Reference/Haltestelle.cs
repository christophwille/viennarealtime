using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    [Table("Haltestellen")]
    public class Haltestelle
    {
        [PrimaryKey]
        public int Id { get; set; }

        // We use the OGD-provided WL_NUMMER as primary key
        // [PrimaryKey, AutoIncrement]
        // public int Id { get; set; }
        // public int WienerLinienNummer { get; set; }

        public string FeatureId { get; set; }

        [Indexed(Name="StationSearchIndex")]
        public string Bezeichnung { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string HaltepunkteIds { get; set; }

        [SQLite.Ignore]
        public string SecondaryInformation
        {
            get { return String.Format("(lat:{0:F2} lon:{1:F2})", Latitude, Longitude); }
        }

        [SQLite.Ignore]
        public double Distanz { get; set; }

        [SQLite.Ignore]
        public string DisplayDistanz 
        { 
            get { return String.Format("{0:F0} m", Distanz); } 
        }

        [SQLite.Ignore]
        public string PreHighlightBlock { get; set; }

        [SQLite.Ignore]
        public string HighlightBlock { get; set; }

        [SQLite.Ignore]
        public string PostHighlightBlock { get; set; }
    }

}
