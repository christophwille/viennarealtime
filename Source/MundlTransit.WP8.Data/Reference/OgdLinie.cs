using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Reference
{
    [Table("OgdLinien")]
    public class OgdLinie
    {
        // We use the OGD-provided LINIEN_ID as primary key
        [PrimaryKey]
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        public int Reihenfolge { get; set; }
        public bool Echtzeit { get; set; }

        [Indexed(Name = "TypeOfVerkehrsmittel")]
        public string Verkehrsmittel { get; set; }

        public string Stand { get; set; }
    }
}
