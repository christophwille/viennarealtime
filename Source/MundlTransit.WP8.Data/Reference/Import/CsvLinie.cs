using System;

namespace MundlTransit.WP8.Data.Reference.Import
{
    //
    // "LINIEN_ID";"BEZEICHNUNG";"REIHENFOLGE";"ECHTZEIT";"VERKEHRSMITTEL";"STAND"
    //
    public class CsvLinie
    {
        public int LINIEN_ID { get; set; }
        public string BEZEICHNUNG { get; set; }
        public int REIHENFOLGE { get; set; }
        public int ECHTZEIT { get; set; }
        public string VERKEHRSMITTEL { get; set; }
        public string STAND { get; set; }
    }
}
