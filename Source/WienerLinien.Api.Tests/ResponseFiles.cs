using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Tests
{
    public static class ResponseFiles
    {
        internal static string LoadJson(string filename)
        {
            return System.IO.File.ReadAllText(filename);
        }

        public static readonly string InvalidKeyResponse = "InvalidKeyResponse.json";
        public static readonly string EmptyOkResponse = "EmptyOkResponse.json";
        public static readonly string RBL2170Hubertusdamm = "RBL2170Hubertusdamm.json";

        // http://www.wienerlinien.at/ogd_realtime/monitor?rbl=4635&rbl=4634&sender=
        public static readonly string U6Siebenhirten = "U6Siebenhirten.json"; 

        // Tscherttegasse: http://www.wienerlinien.at/ogd_realtime/monitor?rbl=4640&rbl=4629&sender=
        public static readonly string TscherttegasseNoOverrides = "TscherttegasseNoOverrides.json"; 
    }
}
