using System;
using System.Collections.Generic;
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
    public class Importer
    {
        const string HaltestellenFile = "wienerlinien-ogd-haltestellen.csv";
        const string LinienFile = "wienerlinien-ogd-linien.csv";
        const string SteigeFile = "wienerlinien-ogd-steige.csv";

        private CsvConfiguration _csvConfiguration;
        private SQLiteConnection _db;

        public void Import(SQLiteConnection db)
        {
            _db = db;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de");

            _csvConfiguration = new CsvConfiguration()
            {
                Delimiter = ","
            };

            ImportHaltestellen();


            _db = null;
            _csvConfiguration = null;
        }

        private void ImportHaltestellen()
        {
            var csv = new CsvReader(new StreamReader(HaltestellenFile, System.Text.Encoding.UTF8), _csvConfiguration);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = new List<Haltestelle>();

            foreach (var hsCsv in haltestellen)
            {
                double longitude, latitude;
                bool ok = ShapeHelper.TryParseShape(hsCsv.SHAPE, out longitude, out latitude);

                if (ok && !String.IsNullOrWhiteSpace(hsCsv.HTXT))
                {
                    var h = new Haltestelle()
                    {
                        Id = hsCsv.OBJECTID,
                        Bezeichnung = hsCsv.HTXT,
                        BezeichnungKurz = hsCsv.HTXTK,
                        Linien = hsCsv.HLINIEN,
                        Longitude = longitude,
                        Latitude = latitude
                    };

                    toInsert.Add(h);
                }
            }

            _db.InsertAll(toInsert);
        }
    }
}
