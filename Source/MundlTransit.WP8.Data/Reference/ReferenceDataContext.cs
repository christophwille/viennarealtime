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
        public const string DatabaseName = "referencedata20131001.db3";

        private static readonly List<string> PreviousDatabases = new List<string>()
        {
            "referencedata20130829.db3",
        }; 

        #region DbPrepare functions
        public static SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(DatabaseName);
        }

        public static void InitializeDatabase()
        {
            using (var db = CreateConnection())
            {
                InitializeDatabase(db);
            }
        }

        public static void InitializeDatabase(SQLiteConnection db)
        {
            db.CreateTable<Haltestelle>();

            db.CreateTable<OgdLinie>();
            db.CreateTable<OgdSteig>();
            db.CreateTable<OgdHaltestelle>();
        }
        #endregion

        private static SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(DatabaseName);
        }

        private readonly SQLiteAsyncConnection _connection;
        public ReferenceDataContext()
        {
            _connection = CreateAsyncConnection();
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
