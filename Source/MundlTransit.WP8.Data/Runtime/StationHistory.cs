using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Runtime
{
    [Table("StationHistory")]
    public class StationHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
