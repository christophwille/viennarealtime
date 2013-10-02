using System;
using System.Diagnostics;
using System.Globalization;

namespace DbPrepare.Ogd
{
    //
    // "HALTESTELLEN_ID";"TYP";"DIVA";"NAME";"GEMEINDE";"GEMEINDE_ID";"WGS84_LAT";"WGS84_LON";"STAND"
    //
    class CsvHaltestelle
    {
        public int HALTESTELLEN_ID { get; set; }
        public string TYP { get; set; }
        public int DIVA { get; set; }
        public string NAME { get; set; }
        public string GEMEINDE { get; set; }
        public int GEMEINDE_ID { get; set; }
        public double WGS84_LAT { get; set; }
        public double WGS84_LON { get; set; }
        public string STAND { get; set; }
    }
}