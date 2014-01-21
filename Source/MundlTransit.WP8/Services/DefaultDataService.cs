using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Model;
using WienerLinien.Api;
using WienerLinien.Api.Realtime;

namespace MundlTransit.WP8.Services
{
    public class DefaultDataService : IDataService
    {
        private readonly IConfigurationService _configurationService;

        public DefaultDataService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        private ReferenceDataContext GetReferenceDataContext()
        {
            bool useDefaultDb = _configurationService.UsingDefaultReferenceDatabase;
            string customDbName = _configurationService.CustomReferenceDatabaseName;
            string dbName = ReferenceDataContext.ReferenceDatabaseName;

            if (!useDefaultDb && !String.IsNullOrWhiteSpace(customDbName))
            {
                dbName = customDbName;
            }

            return new ReferenceDataContext(dbName);
        }

        public async Task<List<Data.Reference.Haltestelle>> GetHaltestellenAsync()
        {
            var ctx = GetReferenceDataContext();
            var hst = await ctx.GetHaltestellenAsync().ConfigureAwait(false);

            return hst;
        }

        public async Task<List<Haltestelle>> GetHaltestellenContainingAsync(string s)
        {
            try
            {
                var ctx = GetReferenceDataContext();
                var result = await ctx.GetHaltestellenContainingAsync(s).ConfigureAwait(false);

                var startingWithString = result
                    .Where(h => h.Bezeichnung.StartsWith(s, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(h => h.Bezeichnung)
                    .ToList();

                foreach (var h in startingWithString)
                {
                    h.HighlightBlock = h.Bezeichnung.Substring(0, s.Length);
                    h.PostHighlightBlock = h.Bezeichnung.Substring(s.Length);
                }

                var containingString = result
                    .Except(startingWithString)
                    .OrderBy(h => h.Bezeichnung)
                    .ToList();

                foreach (var h in containingString)
                {
                    string bezeichnung = h.Bezeichnung; // reduce number of property accesses
                    int pos = bezeichnung.IndexOf(s, StringComparison.CurrentCultureIgnoreCase);

                    h.PreHighlightBlock = bezeichnung.Substring(0, pos);
                    h.HighlightBlock = bezeichnung.Substring(pos, s.Length);
                    h.PostHighlightBlock = bezeichnung.Substring(pos + s.Length);
                }

                startingWithString.AddRange(containingString);
                return startingWithString;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return new List<Haltestelle>();
        }

        //
        // http://stackoverflow.com/questions/3695224/android-sqlite-getting-nearest-locations-with-latitude-and-longitude/12997900#12997900
        //
        public async Task<List<Haltestelle>> GetNearestHaltestellenAsync(Wgs84Location center, double radius = 500.0)
        {
            try
            {
                double mult = 1; // mult = 1.1; is more reliable

                var p1 = NearestLocationHelpers.CalculateDerivedPosition(center, mult * radius, 0);
                var p2 = NearestLocationHelpers.CalculateDerivedPosition(center, mult * radius, 90);
                var p3 = NearestLocationHelpers.CalculateDerivedPosition(center, mult * radius, 180);
                var p4 = NearestLocationHelpers.CalculateDerivedPosition(center, mult * radius, 270);

                var db = GetReferenceDataContext();
                var haltestellen = await db.GetNearestHaltestellenAsync(p3.Latitude, p1.Latitude, p2.Longitude, p4.Longitude).ConfigureAwait(false);

                foreach (var h in haltestellen)
                {
                    h.Distanz = NearestLocationHelpers.GetDistanceBetweenTwoPoints(center, h.Longitude, h.Latitude);
                }

                return haltestellen;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return new List<Haltestelle>();
        }

        public async Task<Haltestelle> GetHaltestelleAsync(int id)
        {
            var db = GetReferenceDataContext();
            var h = await db.GetHaltestelleAsync(id).ConfigureAwait(false);
            return h;
        }

        public async Task<Haltestelle> GetHaltestelleAsync(string name)
        {
            var db = GetReferenceDataContext();
            var h = await db.GetHaltestelleAsync(name).ConfigureAwait(false);
            return h;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync(List<int> ids)
        {
            var db = GetReferenceDataContext();
            var h = await db.GetHaltestellenAsync(ids).ConfigureAwait(false);
            return h;
        }

        public async Task<List<Favorite>> GetFavoritesAsync()
        {
            var db = new RuntimeDataContext();
            var favs = await db.GetFavoritesAsync().ConfigureAwait(false);

            return favs;
        }

        public async Task InsertFavoriteIfNotExistsAsync(Favorite fav)
        {
            var db = new RuntimeDataContext();

            bool exists = await db.DoesFavoriteExistAsync(fav.HaltestellenId).ConfigureAwait(false);
            
            if (!exists)
            {
                await db.InsertFavoriteAsync(fav).ConfigureAwait(false);
            }
        }

        public async Task DeleteFavoriteAsync(Favorite fav)
        {
            var db = new RuntimeDataContext();
            await db.DeleteFavoriteAsync(fav).ConfigureAwait(false);
        }

        public async Task<List<OgdLinie>> GetLinienAsync()
        {
            var db = GetReferenceDataContext();
            return await db.GetLinienAsync().ConfigureAwait(false);
        }

        public async Task<List<OgdLinie>> GetLinienAsync(List<MonitorLineType> mlt)
        {
            var vms = mlt.Select(MonitorLineTypeMapper.TypeToTypeString).ToList();

            var db = GetReferenceDataContext();
            var linien = await db.GetLinienAsync(vms).ConfigureAwait(false);

            return linien.OrderBy(l => l.Reihenfolge).ToList();
        }

        public async Task<List<LinienHaltestelleView>> GetHaltestellenForLinieAsync(int linienId)
        {
            var db = GetReferenceDataContext();
            return await db.GetHaltestellenForLinieAsync(linienId).ConfigureAwait(false);
        }

        public async Task<List<RouteHistoryItem>> GetRouteHistoryItemsAsync()
        {
            var db = new RuntimeDataContext();
            var rhi = await db.GetRouteHistoryItemsAsync().ConfigureAwait(false);

            return rhi;
        }

        public async Task InsertRouteHistoryItemAsync(RouteHistoryItem rhi)
        {
            var db = new RuntimeDataContext();
            await db.InsertRouteHistoryItemAsync(rhi).ConfigureAwait(false);
        }
    }
}
