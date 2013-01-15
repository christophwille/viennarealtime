using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Qando
{
    public class QandoMonitorLine : MonitorLine
    {
        public bool RealtimeSupported { get; set; }

        public List<QandoDeparture> Departures { get; set; } 

        public override string FormatDepartureTimes()
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
                if (RealtimeSupported && d.Countdown.HasValue && d.Countdown.Value < 60)
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
    }
}
