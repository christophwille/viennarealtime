using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPrepare.Ogd
{
    //
    // "LINIEN_ID";"BEZEICHNUNG";"REIHENFOLGE";"ECHTZEIT";"VERKEHRSMITTEL";"STAND"
    //
    class CsvLinie
    {
        public int LINIEN_ID { get; set; }
        public string BEZEICHNUNG { get; set; }
        public int REIHENFOLGE { get; set; }
        public int ECHTZEIT { get; set; }
        public string VERKEHRSMITTEL { get; set; }
        public string STAND { get; set; }
    }
}
