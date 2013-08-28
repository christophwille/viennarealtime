using System;
using System.Diagnostics;
using System.Globalization;

namespace DbPrepare.Ogd
{
    class CsvHaltestelle
    {
        public string FID { get; set; }
        public int OBJECTID { get; set; }
        public string SHAPE { get; set; }
        public string HTXT { get; set; }
        public string HTXTK { get; set; }
        public string HLINIEN { get; set; }
        public string SE_ANNO_CAD_DATA { get; set; }
    }
}