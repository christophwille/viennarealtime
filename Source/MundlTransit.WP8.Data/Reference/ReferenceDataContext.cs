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
        public const string DatabaseName = "referencedata20130113.db3";

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

            var matched = await query.ToListAsync();
            return matched;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync()
        {
            var query = _connection.Table<Haltestelle>();

            var matched = await query.ToListAsync();
            return matched;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync(List<int> ids)
        {
            var query = _connection.Table<Haltestelle>().Where(h => ids.Contains(h.Id));

            var matched = await query.ToListAsync();
            return matched;
        }

        public async Task<Haltestelle> GetHaltestelleAsync(int id)
        {
            var query = _connection.Table<Haltestelle>().Where(h => h.Id == id);

            var matched = await query.ToListAsync();
            return matched.FirstOrDefault();
        }

        public async Task<List<Haltestelle>> GetNearestHaltestellenAsync(double latP3, double latP1, double lonP2, double lonP4)
        {
            var query = _connection.Table<Haltestelle>().Where(h =>
                                                               h.Latitude > latP3 && h.Latitude < latP1 &&
                                                               h.Longitude < lonP2 && h.Longitude > lonP4);

            var matched = await query.ToListAsync();
            return matched;
        }
    }
}
