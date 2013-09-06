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
using WienerLinien.Api.Ogd;

namespace MundlTransit.WP8.Services
{
    public class DefaultDataService : IDataService
    {
        public async Task<List<Data.Reference.Haltestelle>> GetHaltestellenAsync()
        {
            var ctx = new ReferenceDataContext();
            var hst = await ctx.GetHaltestellenAsync().ConfigureAwait(false);

            return hst;
        }

        public async Task<List<Haltestelle>> GetHaltestellenContainingAsync(string s)
        {
            try
            {
                var ctx = new ReferenceDataContext();
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

                var db = new ReferenceDataContext();
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
            var db = new ReferenceDataContext();
            var h = await db.GetHaltestelleAsync(id).ConfigureAwait(false);
            return h;
        }

        public async Task<List<Haltestelle>> GetHaltestellenAsync(List<int> ids)
        {
            var db = new ReferenceDataContext();
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
            var db = new ReferenceDataContext();
            return await db.GetLinienAsync().ConfigureAwait(false);
        }

        public async Task<List<OgdLinie>> GetLinienAsync(List<MonitorLineType> mlt)
        {
            var vms = mlt.Select(MonitorLineTypeMapper.TypeToTypeString).ToList();

            var db = new ReferenceDataContext();
            return await db.GetLinienAsync(vms).ConfigureAwait(false);
        }

        public async Task<List<LinienHaltestelleView>> GetHaltestellenForLinieAsync(int linienId)
        {
            var db = new ReferenceDataContext();
            return await db.GetHaltestellenForLinieAsync(linienId).ConfigureAwait(false);
        }
    }
}
