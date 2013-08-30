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
        Bus,
        SBahn,
        Regionalzug,
        Other,
        BusB,
        NightBus,
        Unknown = 1000,
    }
}
