using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using MundlTransit.WP8.Data.Reference;
using SQLite;

namespace DbPrepare.Ogd
{
    // http://data.gv.at/datensatz/?id=f1f6f15d-2faa-4b62-b78b-80599dd1c66e
    public class HaltestellenImporter
    {
        const string CsvFilename = "Ogd\\OEFFHALTESTOGD.csv";

        public void Import(SQLiteConnection db)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de");

            var config = new CsvConfiguration()
            {
                Delimiter = ","
            };

            var csv = new CsvReader(new StreamReader(CsvFilename, System.Text.Encoding.UTF8), config);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = new List<Haltestelle>();

            foreach (var hsCsv in haltestellen)
            {
                double longitude, latitude;
                bool ok = hsCsv.TryParseShape(out longitude, out latitude);

                if (ok && !String.IsNullOrWhiteSpace(hsCsv.HTXT))
                {
                    var h = new Haltestelle()
                    {
                        Id = hsCsv.OBJECTID,
                        Bezeichnung = hsCsv.HTXT,
                        Longitude = longitude,
                        Latitude = latitude,
                        //HaltepunkteIds = String.Join(",", haltepunkteGroup.Haltepunkte.Select(x => x.haltepunkt))
                    };

                    toInsert.Add(h);
                }
            }

            db.InsertAll(toInsert);
        }
    }

    class CsvHaltestelle
    {
        public string FID { get; set; }
        public int OBJECTID { get; set; }
        public string SHAPE { get; set; }
        public string HTXT { get; set; }
        public string HTXTK { get; set; }
        public string HLINIEN { get; set; }
        public string SE_ANNO_CAD_DATA { get; set; }

        // POINT (16.389769898563777 48.173790150890646)
        public bool TryParseShape(out double longitude, out double latitude)
        {
            try
            {
                string pointShape = SHAPE;

                int openParensPos = pointShape.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);
                string point = pointShape.Substring(++openParensPos, pointShape.Length - openParensPos - 1);

                var lonlat = point.Split(new char[] { ' ' });

                bool lonOk = Double.TryParse(lonlat[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out longitude);
                bool latOk = Double.TryParse(lonlat[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out latitude);

                return (lonOk && latOk);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());

                longitude = 0.0f;
                latitude = 0.0f;
                return false;
            }
        }
    }
}
