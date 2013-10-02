using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    // p54 5.2 Fahrtoptionen (6), Parameter zum Ausschluss von Verkehrsmitteln:
    public enum RoutingTypeOfTransportation
    {
        Walk = 99,
        Zug = 0,
        SBahn = 1,
        UBahn = 2,
        Stadtbahn = 3,
        Tram = 4,
        Stadtbus = 5,
        Regionalbus = 6,
        Schnellbus = 7,
        SeilZahnradbahn = 8,
        Schiff = 9,
        AST_Rufbus = 10,
        Sonstige = 11
    }
}
