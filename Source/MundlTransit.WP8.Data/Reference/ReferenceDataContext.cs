using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    public partial class ReferenceDataContext
    {
        // http://data.wien.gv.at/csv/wienerlinien-ogd-version.csv
        // GUELTIG_AB;"ERSTELLT_AM"
        // 2015-04-01;"2015-04-01 07:18:51"
        public const string ReferenceDatabaseName = "referencedata20150401.db3";
        public static readonly DateTime ReferenceDatabaseBuildDate = new DateTime(2015, 04, 01);

        private static SQLiteAsyncConnection CreateAsyncConnection(string databaseName)
        {
            return new SQLiteAsyncConnection(databaseName);
        }

        private readonly SQLiteAsyncConnection _connection;
        public ReferenceDataContext() : this(ReferenceDatabaseName) { }

        public ReferenceDataContext(string databaseName)
        {
            _connection = CreateAsyncConnection(databaseName);
        }

        public async Task<List<Haltestelle>> GetHaltestellenContainingAsync(string s)
        {
            var query = _connection
                                .Table<Haltestelle>()
                                .Where(h => h.Bezeichnung.Contains(s));

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync()
        {
            var query = _connection.Table<Haltestelle>();

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync(List<int> ids)
        {
            var query = _connection.Table<Haltestelle>().Where(h => ids.Contains(h.Id));

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<Haltestelle> GetHaltestelleAsync(int id)
        {
            var query = _connection.Table<Haltestelle>().Where(h => h.Id == id);

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched.FirstOrDefault();
        }

        public async Task<Haltestelle> GetHaltestelleAsync(string name)
        {
            var query = _connection.Table<Haltestelle>().Where(h => h.Bezeichnung == name);

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched.FirstOrDefault();
        }

        public async Task<List<Haltestelle>> GetNearestHaltestellenAsync(double latP3, double latP1, double lonP2, double lonP4)
        {
            var query = _connection.Table<Haltestelle>().Where(h =>
                                                               h.Latitude > latP3 && h.Latitude < latP1 &&
                                                               h.Longitude < lonP2 && h.Longitude > lonP4);

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<List<OgdLinie>> GetLinienAsync()
        {
            var query = _connection.Table<OgdLinie>();

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<List<OgdLinie>> GetLinienAsync(List<string> vms)
        {
            var query = _connection.Table<OgdLinie>().Where(l => vms.Contains(l.Verkehrsmittel.ToLower()));

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        private const string SqlForLineStations = 
                @"SELECT Haltestellen.*, OgdSteige.Richtung, OgdSteige.Reihenfolge
                    FROM  OgdSteige INNER JOIN
                                OgdLinien ON OgdSteige.FK_LinienId = OgdLinien.Id INNER JOIN
                                Haltestellen ON OgdSteige.FK_HaltestellenId = Haltestellen.Id
                    WHERE OgdLinien.Id = {0}
                    ORDER BY OgdSteige.Richtung, OgdSteige.Reihenfolge";

        public async Task<List<LinienHaltestelleView>> GetHaltestellenForLinieAsync(int linienId)
        {
            var result = await _connection.QueryAsync<LinienHaltestelleView>(String.Format(SqlForLineStations, linienId)).ConfigureAwait(false);
            return result;
        }
    }
}
