using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Ogd
{
    public static class MonitorLineTypeMapper
    {
        public static MonitorLineType TypeStringToType(string type)
        {
            var retType = MonitorLineType.Unknown;

            switch (type.ToLowerInvariant())
            {
                case "ptmetro":
                    retType = MonitorLineType.Metro;
                    break;
                case "pttram":
                    retType = MonitorLineType.Tram;
                    break;
                case "ptbuscity":
                    retType = MonitorLineType.Bus;
                    break;
                case "pttrains":
                    retType = MonitorLineType.SBahn;
                    break;
                case "pttrainr":
                    retType = MonitorLineType.Regionalzug;
                    break;
                case "other":
                    retType = MonitorLineType.Other;
                    break;

            }

            return retType;
        }
    }
}
