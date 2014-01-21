using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    public partial class ReferenceDataContext
    {
        public async Task InitializeDatabaseAsync()
        {
            await _connection
                .CreateTablesAsync<Haltestelle,OgdLinie,OgdSteig,OgdHaltestelle>()
                .ConfigureAwait(false);
        }

        public async Task InsertAsync(IEnumerable objects)
        {
            await _connection.InsertAllAsync(objects).ConfigureAwait(false);
        }

        private static string LinienAtHaltestelleQuery =
            @"SELECT OgdLinien.Id, OgdLinien.Bezeichnung, OgdSteige.RblNummer
                            FROM OgdLinien
                            INNER JOIN OgdSteige
                            ON OgdLinien.Id = OgdSteige.FK_LinienId
                            WHERE OgdSteige.FK_HaltestellenId = {0}
                            ORDER BY OgdLinien.Reihenfolge";

        // GROUP BY OgdLinien.Id, OgdLinien.Bezeichnung

        public async Task<int> CreateLookupTableAsync()
        {
            var haltestellen = await _connection.Table<OgdHaltestelle>()
                .ToListAsync()
                .ConfigureAwait(false);

            var toInsert = new List<Haltestelle>();

            foreach (var h in haltestellen)
            {
                var result = await _connection
                    .QueryAsync<LinienAtHaltestelleModel>(String.Format(LinienAtHaltestelleQuery, h.Id))
                    .ConfigureAwait(false);

                var groupedLinien = (from model in result
                                     group model by new { model.Id, model.Bezeichnung }
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
                        Diva = h.Diva,
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
                    Debug.WriteLine(h.Bezeichnung + " was omitted because of missing Steige");
                }
            }

            return await _connection.InsertAllAsync(toInsert).ConfigureAwait(false);
        }
    }
}
