using System;
using System.Collections.Generic;
using System.Linq;
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
                    @"SELECT OgdLinien.Id, OgdLinien.Bezeichnung
                            FROM OgdLinien
                            INNER JOIN OgdSteige
                            ON OgdLinien.Id = OgdSteige.FK_LinienId
                            WHERE OgdSteige.FK_HaltestellenId = {0}
                            GROUP BY OgdLinien.Id, OgdLinien.Bezeichnung
                            ORDER BY OgdLinien.Reihenfolge";

        class LinienAtHaltestelleModel
        {
            public int Id { get; set; }
            public string Bezeichnung { get; set; }
        }

        private static void CreateLookupTable(SQLiteConnection db)
        {
            var haltestellen = db.Table<OgdHaltestelle>().ToList();
            var toInsert = new List<Haltestelle>();

            foreach (var h in haltestellen)
            {
                var result = db.Query<LinienAtHaltestelleModel>(String.Format(JoinQuery, h.Id));

                string linienDisplay = String.Join(", ", result.Select(r => r.Bezeichnung));
                string linienIds = String.Join(",", result.Select(r => r.Id));

                var haltstelle = new Haltestelle()
                {
                    Id = h.Id,
                    Bezeichnung = h.Bezeichnung,
                    Longitude = h.Longitude,
                    Latitude = h.Latitude,
                    Linien = linienDisplay,
                    LinienIds = linienIds
                };

                toInsert.Add(haltstelle);
            }

            db.InsertAll(toInsert);
        }
    }
}
