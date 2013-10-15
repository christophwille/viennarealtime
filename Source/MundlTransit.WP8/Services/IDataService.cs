using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Model;
using WienerLinien.Api;

namespace MundlTransit.WP8.Services
{
    public interface IDataService
    {
        // Reference Data
        Task<List<Haltestelle>> GetHaltestellenContainingAsync(string s);
        Task<List<Haltestelle>> GetHaltestellenAsync();
        Task<List<Haltestelle>> GetNearestHaltestellenAsync(Wgs84Location center, double radius = 500.0);
        Task<Haltestelle> GetHaltestelleAsync(int id);
        Task<List<Haltestelle>> GetHaltestellenAsync(List<int> ids);

        Task<List<OgdLinie>> GetLinienAsync();
        Task<List<OgdLinie>> GetLinienAsync(List<MonitorLineType> mlt);

        Task<List<LinienHaltestelleView>> GetHaltestellenForLinieAsync(int linienId);

        // Runtime Data
        Task<List<Favorite>> GetFavoritesAsync();
        Task InsertFavoriteIfNotExistsAsync(Favorite fav);
        Task DeleteFavoriteAsync(Favorite fav);

        Task<List<RouteHistoryItem>> GetRouteHistoryItemsAsync();
        Task InsertRouteHistoryItemAsync(RouteHistoryItem rhi);
    }
}
