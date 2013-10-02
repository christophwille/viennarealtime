using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Tests
{
    public static class ResponseFiles
    {
        internal static string Load(string filename)
        {
            return System.IO.File.ReadAllText(filename);
        }

        internal static string LoadJson(string filename)
        {
            return Load(filename);
        }

        public static readonly string InvalidKeyResponse = "InvalidKeyResponse.json";
        public static readonly string EmptyOkResponse = "EmptyOkResponse.json";
        public static readonly string RBL2170Hubertusdamm = "RBL2170Hubertusdamm.json";

        // http://www.wienerlinien.at/ogd_realtime/monitor?rbl=4635&rbl=4634&sender=
        public static readonly string U6Siebenhirten = "U6Siebenhirten.json"; 

        // Tscherttegasse: http://www.wienerlinien.at/ogd_realtime/monitor?rbl=4640&rbl=4629&sender=
        public static readonly string TscherttegasseNoOverrides = "TscherttegasseNoOverrides.json";

        // Stoerunglang: http://www.wienerlinien.at/ogd_realtime/trafficInfoList?name=stoerunglang&sender=
        public static readonly string Stoerunglang = "Stoerunglang.json";



        // ROUTING

        // p56 5.2 Fahrtoptionen (8): http://www.wienerlinien.at/ogd_routing/XML_TRIP_REQUEST2?locationServerActive=1&type_origin=any&name_origin=Westbahnhof&type_destination=any&name_destination=Stephansplatz&ptOptionsActive=1&excludedMeans=4
        public static readonly string RoutingFahrtoptionen8 = "RoutingFahrtoptionen8.xml";
    }
}
