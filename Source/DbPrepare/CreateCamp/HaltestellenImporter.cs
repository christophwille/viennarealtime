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
using CsvHelper.Configuration;
using System.Threading;

namespace DbPrepare.CreateCamp
{
    class CsvHaltestelle
    {
        public int haltepunkt { get; set; }
        public int haltestelle { get; set; }
        public string kurzbez { get; set; }
        public string langbez { get; set; }
        public string bezirk { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
   }

    public class HaltestellenImporter
    {
        const string CsvFilename = "CreateCamp\\WL_Haltepunkte_CreateCamp_12012013 mit Koordinaten.csv";

        public void Import(SQLiteConnection db)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de");

            var config = new CsvConfiguration()
                             {
                                 Delimiter = ';'
                             };

            var csv = new CsvReader(new StreamReader(CsvFilename), config);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var query = from h in haltestellen
                        group h by h.haltestelle
                            into g
                            select new
                            {
                                HaltestellenId = g.Key,
                                Haltepunkte = g.ToList()
                            };

            var toInsert = new List<Haltestelle>();

            foreach (var haltepunkteGroup in query)
            {
                var firstHaltepunkt =  haltepunkteGroup.Haltepunkte.First();

                var h = new Haltestelle()
                            {
                                Id = firstHaltepunkt.haltestelle,
                                Bezeichnung = firstHaltepunkt.langbez,
                                Longitude = firstHaltepunkt.longitude,
                                Latitude = firstHaltepunkt.latitude,
                                HaltepunkteIds = String.Join(",", haltepunkteGroup.Haltepunkte.Select(x => x.haltepunkt))
                            };

                toInsert.Add(h);
            }

            db.InsertAll(toInsert);
        }
    }
}
