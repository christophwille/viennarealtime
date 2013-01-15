using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Runtime
{
    [Table("Favorites")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "HaltestellenIdIndex")]
        public int HaltestellenId { get; set; }

        // Intentional duplication from Reference Data
        public string Bezeichnung { get; set; }
    }
}
