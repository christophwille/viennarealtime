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
        // We use the OGD-provided OBJECTID as primary key
        [PrimaryKey]
        public int Id { get; set; }

        public string FeatureId { get; set; }

        [Indexed(Name="StationSearchIndex")]
        public string Bezeichnung { get; set; }

        public string BezeichnungKurz { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Linien { get; set; }

        [SQLite.Ignore]
        public string SecondaryInformation
        {
            get { return Linien; }
        }

        private string FormatLatLonAsInfo()
        {
            return String.Format("(lat:{0:F2} lon:{1:F2})", Latitude, Longitude);
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
