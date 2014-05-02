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
using MundlTransit.WP8.Model;

namespace MundlTransit.WP8.Services
{
    // https://open.wien.at/site/datensatz/?id=add66f20-d033-4eee-b9a0-47019828e698
    public class DefaultImportService
    {
        const string HaltestellenUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-haltestellen.csv";
        const string LinienUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-linien.csv";
        const string SteigeUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-steige.csv";

        const string VersionUrl = "http://data.wien.gv.at/csv/wienerlinien-ogd-version.csv";

        private CsvConfiguration _csvConfiguration;
        private ReferenceDataContext _ctx;

        public DefaultImportService(ReferenceDataContext ctx)
        {
            _ctx = ctx;

            _csvConfiguration = new CsvConfiguration()
            {
                Delimiter = ";",
                CultureInfo = new CultureInfo("en"),
                HasHeaderRecord = true,
                IgnoreReadingExceptions = true
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

        public async Task<ImportResults> ImportBatchAsync()
        {
            var result = new ImportResults();

            string haltestellen = await DownloadHaltestellenAsync().ConfigureAwait(false);
            string linien = await DownloadLinienAsync().ConfigureAwait(false);
            string steige = await DownloadSteigeAsync().ConfigureAwait(false);

            if (null == haltestellen || null == linien || null == steige)
            {
                result.ErrorMessage ="Error downloading reference CSV files from data.wien.gv.at";
                return result;
            }

            result.HaltestellenCount = await ImportHaltestellenAsync(haltestellen).ConfigureAwait(false);
            result.LinienCount = await ImportLinienAsync(linien).ConfigureAwait(false);
            result.SteigeCount = await ImportSteigeAsync(steige).ConfigureAwait(false);
            result.Succeeded = true;

            return result;
        }

        public async Task<int> CreateLookupTableAsync()
        {
            return await _ctx.CreateLookupTableAsync().ConfigureAwait(false);
        }

        private TextReader GetAsTextReader(string data)
        {
            return new StringReader(data);
        }

        public async Task<int> ImportHaltestellenAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);

            //
            // Debug helper, uncomment for detecting format changes
            //

            //CsvHaltestelle record = null; 
            //while (csv.Read())
            //{
            //    try
            //    {
            //        record = csv.GetRecord<CsvHaltestelle>();
            //    }
            //    catch (Exception)
            //    {
            //        Debug.WriteLine("Previous record id: " + record.HALTESTELLEN_ID);
            //    }
            //}

            // IgnoreReadingExceptions = true is fix for errneous data in 21.4.2014 data release where three stations have no GPS coordinates
            //
            //214465598;"stop";60208499;"Südtiroler Platz (Fahrziele)";"Wien";90000;"";"";"2013-12-18 09:14:05"
            //214465599;"stop";60208500;"Südbahnhof (Fahrziele)";"Wien";90000;"";"";"2013-12-18 09:14:05"
            //214465631;"stop";60209900;"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";"Wien";90000;"";"";"2013-12-18 09:14:05"
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = CsvToOgd.ConvertHaltestellen(haltestellen);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);

            return toInsert.Count;
        }

        public async Task<int> ImportLinienAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);

            var linien = csv.GetRecords<CsvLinie>().ToList();

            var toInsert = CsvToOgd.ConvertLinien(linien);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);

            return toInsert.Count;
        }

        public async Task<int> ImportSteigeAsync(string data)
        {
            var csv = new CsvReader(GetAsTextReader(data), _csvConfiguration);
            var steige = csv.GetRecords<CsvSteig>().ToList();

            var toInsert = CsvToOgd.ConvertSteige(steige);

            await _ctx.InsertAsync(toInsert).ConfigureAwait(false);

            return toInsert.Count;
        }
    }
}
