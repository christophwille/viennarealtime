using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public class MonitorLine : ILineInformation
    {
        public string Name { get; set; }
        public string Towards { get; set; }
        public MonitorLineType Type { get; set; }
        public bool RealtimeSupported { get; set; }
        public bool BarrierFree { get; set; }

        public List<Departure> Departures { get; set; } 

        public virtual string FormatLineInformation()
        {
            return String.Format("{0} ({1})", Name, Towards);
        }

        public string LineInformation
        {
            get { return FormatLineInformation(); }
            set { }
        }

        public virtual string FormatDepartureTimes()
        {
            if (null == Departures) return "";

            var stb = new StringBuilder();
            bool first = true;
            int maxItems = 4;               // Limit # of departure times shown

            foreach (var d in Departures)
            {
                if (maxItems-- == 0) break;

                if (!first) stb.Append(", ");

                // Show minutes if: realtime information available, time to departure less than 60 minutes
                if (RealtimeSupported && d.Countdown < 60)
                {
                    stb.Append(d.Countdown);
                }
                else
                {
                    stb.AppendFormat("{0:H:mm}", d.TimePlanned);
                }

                first = false;
            }

            return stb.ToString();
        }

        public string DepartureTimes
        {
            get { return FormatDepartureTimes(); }
            set { }
        }
    }
}
