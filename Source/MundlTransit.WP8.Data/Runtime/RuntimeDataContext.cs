using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;

namespace MundlTransit.WP8.Data.Runtime
{
    public class RuntimeDataContext
    {
        private const string DatabaseName = "runtime.db3";

        // Explicitly request the Local folder (Reference data: not necessary as it is expressly copied to this location)
        private static SQLiteAsyncConnection CreateConnection()
        {
            var localFolder = ApplicationData.Current.LocalFolder.Path;
            return new SQLiteAsyncConnection(localFolder + "\\" + DatabaseName);
        }

        public static async Task InitializeDatabaseAsync()
        {
            var db = CreateConnection();

            CreateTablesResult favCreateResult = await db.CreateTableAsync<Favorite>().ConfigureAwait(false);
            // CreateTablesResult histCreateResult = await db.CreateTableAsync<StationHistory>();
        }

        private readonly SQLiteAsyncConnection _connection;
        public RuntimeDataContext()
        {
            _connection = CreateConnection();
        }

        public async Task<List<Favorite>> GetFavoritesAsync()
        {
            var query = _connection
                .Table<Favorite>()
                .OrderBy(f => f.Bezeichnung);

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched;
        }

        public async Task<bool> DoesFavoriteExistAsync(int haltenStellenId)
        {
            var query = _connection
                .Table<Favorite>()
                .Where(f => f.HaltestellenId == haltenStellenId);

            var matched = await query.ToListAsync().ConfigureAwait(false);
            return matched.Any();
        }

        public async Task InsertFavoriteAsync(Favorite fav)
        {
            int result = await _connection.InsertAsync(fav).ConfigureAwait(false);
        }

        public async Task DeleteFavoriteAsync(Favorite fav)
        {
            int result = await _connection.DeleteAsync(fav).ConfigureAwait(false);
        }
    }
}
