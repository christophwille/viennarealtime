using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Reference.Import;

namespace DbPrepare.Ogd
{
    // http://data.gv.at/datensatz/?id=add66f20-d033-4eee-b9a0-47019828e698
    public class Importer
    {
        const string HaltestellenFile = "wienerlinien-ogd-haltestellen.csv";
        const string LinienFile = "wienerlinien-ogd-linien.csv";
        const string SteigeFile = "wienerlinien-ogd-steige.csv";

        private CsvConfiguration _csvConfiguration;
        private ReferenceDataContext _ctx;

        public async Task ImportAsync(ReferenceDataContext ctx)
        {
            _ctx = ctx;

            _csvConfiguration = new CsvConfiguration()
            {
                Delimiter = ";"
            };

            await ImportHaltestellenAsync();
            await ImportLinienAsync();
            await ImportSteigeAsync();
        }

        private StreamReader GetStream(string filename)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");

            return 
                new StreamReader("../../" + filename, System.Text.Encoding.UTF8);
        }

        private async Task ImportHaltestellenAsync()
        {
            var csv = new CsvReader(GetStream(HaltestellenFile), _csvConfiguration);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = CsvToOgd.ConvertHaltestellen(haltestellen);

            await _ctx.InsertAsync(toInsert);
        }

        private async Task ImportLinienAsync()
        {
            var csv = new CsvReader(GetStream(LinienFile), _csvConfiguration);
            var linien = csv.GetRecords<CsvLinie>().ToList();

            var toInsert = CsvToOgd.ConvertLinien(linien);

            await _ctx.InsertAsync(toInsert);
        }

        private async Task ImportSteigeAsync()
        {
            var csv = new CsvReader(GetStream(SteigeFile), _csvConfiguration);
            var steige = csv.GetRecords<CsvSteig>().ToList();

            var toInsert = CsvToOgd.ConvertSteige(steige);

            await _ctx.InsertAsync(toInsert);
        }
    }
}
