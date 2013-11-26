using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Reference.Import;

namespace MundlTransit.WP8.Services
{
    // http://data.gv.at/datensatz/?id=add66f20-d033-4eee-b9a0-47019828e698
    public class DefaultImportService
    {
        const string HaltestellenUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-haltestellen.csv";
        const string LinienUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-linien.csv";
        const string SteigeUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-steige.csv";

        private CsvConfiguration _csvConfiguration;
        private ReferenceDataContext _ctx;

        public DefaultImportService(ReferenceDataContext ctx)
        {
            _ctx = ctx;

            _csvConfiguration = new CsvConfiguration()
            {
                Delimiter = ";",
                CultureInfo = new CultureInfo("en")
            };
        }

        public async Task<string> DownloadHaltestellenAsync()
        {
            return await DownloadClient.GetAsStringAsync(HaltestellenUrl).ConfigureAwait(false);
        }

        public async Task<string> DownloadLinienAsync()
        {
            return await DownloadClient.GetAsStringAsync(LinienUrl).ConfigureAwait(false);
        }

        public async Task<string> DownloadSteigeAsync()
        {
            return await DownloadClient.GetAsStringAsync(SteigeUrl).ConfigureAwait(false);
        }

        public async Task ImportBatchAsync()
        {
            string haltestellen = await DownloadHaltestellenAsync().ConfigureAwait(false);
            string linien = await DownloadLinienAsync().ConfigureAwait(false);
            string steige = await DownloadSteigeAsync().ConfigureAwait(false);

            if (null == haltestellen || null == linien || null == steige)
            {
                Debug.WriteLine("Error downloading reference CSV files from data.wien.gv.at");
                return;
            }

            await ImportHaltestellenAsync(haltestellen).ConfigureAwait(false);
            await ImportLinienAsync(linien).ConfigureAwait(false);
            await ImportSteigeAsync(steige).ConfigureAwait(false);
        }

        public async Task CreateLookupTableAsync()
        {
            await _ctx.CreateLookupTableAsync().ConfigureAwait(false);
        }

        private TextReader GetAsTextReader(string data)
        {
            return new StringReader(data);
        }

        public async Task ImportHaltestellenAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = CsvToOgd.ConvertHaltestellen(haltestellen);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);
        }

        public async Task ImportLinienAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);
            var linien = csv.GetRecords<CsvLinie>().ToList();

            var toInsert = CsvToOgd.ConvertLinien(linien);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);
        }

        public async Task ImportSteigeAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);
            var steige = csv.GetRecords<CsvSteig>().ToList();

            var toInsert = CsvToOgd.ConvertSteige(steige);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);
        }
    }
}
