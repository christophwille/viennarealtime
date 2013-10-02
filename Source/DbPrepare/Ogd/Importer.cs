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
using SQLite;

namespace DbPrepare.Ogd
{
    // http://data.gv.at/datensatz/?id=f1f6f15d-2faa-4b62-b78b-80599dd1c66e
    public class Importer
    {
        const string HaltestellenFile = "wienerlinien-ogd-haltestellen.csv";
        const string LinienFile = "wienerlinien-ogd-linien.csv";
        const string SteigeFile = "wienerlinien-ogd-steige.csv";

        private CsvConfiguration _csvConfiguration;
        private SQLiteConnection _db;

        public void Import(SQLiteConnection db)
        {
            _db = db;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");

            _csvConfiguration = new CsvConfiguration()
            {
                Delimiter = ";"
            };

            ImportHaltestellen();
            ImportLinien();
            ImportSteige();

            _db = null;
            _csvConfiguration = null;
        }

        private StreamReader GetStream(string filename)
        {
            return new StreamReader("../../" + filename, System.Text.Encoding.UTF8);
        }

        private void ImportHaltestellen()
        {
            var csv = new CsvReader(GetStream(HaltestellenFile), _csvConfiguration);
            var haltestellen = csv.GetRecords<CsvHaltestelle>().ToList();

            var toInsert = haltestellen.Select(hsCsv => new OgdHaltestelle()
                                {
                                    Id = hsCsv.HALTESTELLEN_ID,
                                    Diva = hsCsv.DIVA,
                                    Bezeichnung = hsCsv.NAME,
                                    Longitude = hsCsv.WGS84_LON,
                                    Latitude = hsCsv.WGS84_LAT,
                                    Stand = hsCsv.STAND
                                })
                                .ToList();

            _db.InsertAll(toInsert);
        }

        private void ImportLinien()
        {
            var csv = new CsvReader(GetStream(LinienFile), _csvConfiguration);
            var linien = csv.GetRecords<CsvLinie>().ToList();

            var toInsert = linien.Select(lCsv => new OgdLinie()
                                {
                                    Id = lCsv.LINIEN_ID,
                                    Reihenfolge = lCsv.REIHENFOLGE,
                                    Bezeichnung = lCsv.BEZEICHNUNG,
                                    Echtzeit = lCsv.ECHTZEIT == 1,
                                    Verkehrsmittel = lCsv.VERKEHRSMITTEL,
                                    Stand = lCsv.STAND
                                })
                                .ToList();

            _db.InsertAll(toInsert);
        }

        private void ImportSteige()
        {
            var csv = new CsvReader(GetStream(SteigeFile), _csvConfiguration);
            var steige = csv.GetRecords<CsvSteig>().ToList();

            var toInsert = steige.Select(sCsv => new OgdSteig()
                            {
                                Id = sCsv.STEIG_ID,
                                FK_LinienId = sCsv.FK_LINIEN_ID,
                                FK_HaltestellenId = sCsv.FK_HALTESTELLEN_ID,
                                Richtung = sCsv.RICHTUNG,
                                Reihenfolge = sCsv.REIHENFOLGE,
                                RblNummer = sCsv.RBL_NUMMER,
                                Bereich = sCsv.BEREICH,
                                Name = sCsv.STEIG,
                                Latitude = sCsv.STEIG_WGS84_LAT,
                                Longitude = sCsv.STEIG_WGS84_LON,
                                Stand = sCsv.STAND,
                            })
                            .ToList();

            _db.InsertAll(toInsert);
        }
    }
}
