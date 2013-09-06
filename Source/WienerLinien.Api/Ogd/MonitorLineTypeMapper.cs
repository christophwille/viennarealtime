using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Ogd
{
    public static class MonitorLineTypeMapper
    {
        public const string TypeMetro = "ptmetro";
        public const string TypeTram = "pttram";
        public const string TypeTramWLB = "pttramwlb";
        public const string TypeBus = "ptbuscity";
        public const string TypeNightBus = "ptbusnight";
        public const string TypeTrainS = "pttrains";

        public static MonitorLineType TypeStringToType(string type)
        {
            var retType = MonitorLineType.Unknown;

            switch (type.ToLowerInvariant())
            {
                case TypeMetro:
                    retType = MonitorLineType.Metro;
                    break;
                case TypeTram:
                    retType = MonitorLineType.Tram;
                    break;
                case TypeTramWLB:
                    retType = MonitorLineType.TramWLB;
                    break;
                case TypeBus:
                    retType = MonitorLineType.Bus;
                    break;
                case TypeTrainS:
                    retType = MonitorLineType.SBahn;
                    break;
                case TypeNightBus:
                    retType = MonitorLineType.NightBus;
                    break;
                case "other":
                    retType = MonitorLineType.Other;
                    break;
            }

            return retType;
        }

        public static string TypeToTypeString(MonitorLineType mlt)
        {
            var retType = "";

            switch (mlt)
            {
                case MonitorLineType.Metro:
                    retType = TypeMetro;
                    break;
                case MonitorLineType.Tram:
                    retType = TypeTram;
                    break;
                case MonitorLineType.TramWLB:
                    retType = TypeTramWLB;
                    break;
                case MonitorLineType.Bus:
                    retType = TypeBus;
                    break;
                case MonitorLineType.NightBus:
                    retType = TypeNightBus;
                    break;
            }

            return retType;
        }
    }
}
