using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MundlTransit.WP8.Data.Reference;
using SQLite;

namespace DbPrepare.Qando
{
    // NOTE: .csv must be saved as UNICODE UTF-8, otherwise special characters won't show properly
    class CsvHaltestelle
    {
        public string FID { get; set; }
        public string SHAPE { get; set; }
        public string BEZEICHNUNG { get; set; }
        public int WL_NUMMER { get; set; }

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

    //
    // Uses: https://github.com/JoshClose/CsvHelper/wiki/Basics
    //
    // Other options:
    // http://www.filehelpers.com/
    // http://www.codeproject.com/Articles/9258/A-Fast-CSV-Reader
    //
    public class HaltestellenImporter
    {
        const string CsvFilename = "Qando\\HALTESTELLEWLOGD.csv";

        public void Import(SQLiteConnection db)
        {
            var csv = new CsvReader(new StreamReader(CsvFilename));
            var haltestellen = csv.GetRecords<CsvHaltestelle>();

            var toInsert = new List<Haltestelle>();

            foreach (var haltestelle in haltestellen)
            {
                double longitude, latitude;
                bool ok = haltestelle.TryParseShape(out longitude, out latitude);

                if (ok && !String.IsNullOrWhiteSpace(haltestelle.BEZEICHNUNG))
                {
                    toInsert.Add(new Haltestelle()
                    {
                        Id = haltestelle.WL_NUMMER,
                        Bezeichnung = haltestelle.BEZEICHNUNG,
                        Longitude = longitude,
                        Latitude = latitude
                    });
                }
            }

            db.InsertAll(toInsert);
        }
    }
}
