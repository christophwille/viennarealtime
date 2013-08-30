using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using SQLite;

namespace DbPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clean up first
            System.IO.File.Delete(ReferenceDataContext.DatabaseName);

            // Re-initialize
            SQLiteConnection db = ReferenceDataContext.CreateConnection();
            ReferenceDataContext.InitializeDatabase(db);

            // Perform import
            var importer = new Ogd.Importer();
            importer.Import(db);

            CreateLookupTable(db);
        }

        private static string JoinQuery =
                    @"SELECT OgdLinien.Id, OgdLinien.Bezeichnung, OgdSteige.RblNummer
                            FROM OgdLinien
                            INNER JOIN OgdSteige
                            ON OgdLinien.Id = OgdSteige.FK_LinienId
                            WHERE OgdSteige.FK_HaltestellenId = {0}
                            ORDER BY OgdLinien.Reihenfolge";

        // GROUP BY OgdLinien.Id, OgdLinien.Bezeichnung

        class LinienAtHaltestelleModel
        {
            public int Id { get; set; }
            public string Bezeichnung { get; set; }
            public string RblNummer { get; set; }
        }

        private static void CreateLookupTable(SQLiteConnection db)
        {
            var haltestellen = db.Table<OgdHaltestelle>().ToList();
            var toInsert = new List<Haltestelle>();

            foreach (var h in haltestellen)
            {
                var result = db.Query<LinienAtHaltestelleModel>(String.Format(JoinQuery, h.Id));

                var groupedLinien = (from model in result
                    group model by  new { model.Id, model.Bezeichnung }
                    into g
                    select new { g.Key.Id, g.Key.Bezeichnung }).ToList();

                string linienDisplay = String.Join(", ", groupedLinien.Select(r => r.Bezeichnung));
                string linienIds = String.Join(",", groupedLinien.Select(r => r.Id));

                // Take only records where RblNummer exists
                string rblNummern = String.Join(",", result
                    .Where(r => !String.IsNullOrWhiteSpace(r.RblNummer))
                    .Select(r => r.RblNummer));

                // eg.Abdsdorf-Hippersdorf S Bahn has no Steige
                if (!String.IsNullOrWhiteSpace(rblNummern))
                {
                    var haltstelle = new Haltestelle()
                    {
                        Id = h.Id,
                        Bezeichnung = h.Bezeichnung,
                        Longitude = h.Longitude,
                        Latitude = h.Latitude,
                        Linien = linienDisplay,
                        LinienIds = linienIds,
                        RblNummern = rblNummern
                    };

                    toInsert.Add(haltstelle);
                }
                else
                {
                    Console.WriteLine(h.Bezeichnung + " was omitted because of missing Steige");
                }
            }

            db.InsertAll(toInsert);
        }
    }
}
