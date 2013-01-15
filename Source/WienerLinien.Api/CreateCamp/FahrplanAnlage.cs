using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.CreateCamp
{
    public class FahrplanAnlage
    {
        public string Line { get; set; }
        public string Towards { get; set; }
        public DateTime? Realtime { get; set; }
        public MonitorLineType Type { get; set; }
    }
}
