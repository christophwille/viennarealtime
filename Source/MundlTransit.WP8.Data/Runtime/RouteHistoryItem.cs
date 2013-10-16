using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MundlTransit.WP8.Data.Runtime
{
    [Table("RouteHistoryItems")]
    public class RouteHistoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int? FromHaltestelleId { get; set; }
        public int? ToHaltestelleId { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public int RouteType { get; set; }
    }
}
