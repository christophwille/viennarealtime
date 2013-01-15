using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public enum MonitorLineType
    {
        Unknown = 0,
        Metro,
        Tram,
        Bus,
        SBahn,
        Regionalzug,
        Other,
        BusB,
        NightBus
    }
}
