using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Data.Reference.Import
{
    public static class CsvToOgd
    {
        public static List<OgdHaltestelle> ConvertHaltestellen(List<CsvHaltestelle> haltestellen)
        {
            return haltestellen.Select(hsCsv => new OgdHaltestelle()
                                    {
                                        Id = hsCsv.HALTESTELLEN_ID,
                                        Diva = hsCsv.DIVA,
                                        Bezeichnung = hsCsv.NAME,
                                        Longitude = hsCsv.WGS84_LON,
                                        Latitude = hsCsv.WGS84_LAT,
                                        Stand = hsCsv.STAND
                                    })
                                    .ToList();
        }

        public static List<OgdLinie> ConvertLinien(List<CsvLinie> linien)
        {
            return linien.Select(lCsv => new OgdLinie()
                            {
                                Id = lCsv.LINIEN_ID,
                                Reihenfolge = lCsv.REIHENFOLGE,
                                Bezeichnung = lCsv.BEZEICHNUNG,
                                Echtzeit = lCsv.ECHTZEIT == 1,
                                Verkehrsmittel = lCsv.VERKEHRSMITTEL,
                                Stand = lCsv.STAND
                            })
                            .ToList();
        }

        public static List<OgdSteig> ConvertSteige(List<CsvSteig> steige)
        {
            return steige.Select(sCsv => new OgdSteig()
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
        }
    }
}
