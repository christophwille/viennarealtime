using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public enum MonitorLineType
    {
        Metro = 1,
        Tram,
        TramWLB,
        Bus,
        SBahn,
        Other,
        NightBus,
        Unknown = 1000,
    }
}
