using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using WienerLinien.Api;
using WienerLinien.Api.Ogd;

namespace MundlTransit.WP8.DesignTimeData
{
    public class SampleLineData
    {
        public MonitorLineType LineType { get; set; }
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public int Reihenfolge { get; set; }
        public bool Echtzeit { get; set; }
    }
}
