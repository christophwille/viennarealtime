using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public class Departure : ILineInformation
    {
        public bool OverridesLineInformation { get; set; }
        public string Name { get; set; }
        public string Towards { get; set; }
        public MonitorLineType Type { get; set; }
        public bool RealtimeSupported { get; set; }
        public bool BarrierFree { get; set; }

        public int Countdown { get; set; }
        public DateTime? TimePlanned { get; set; }
        public DateTime? TimeReal { get; set; }

        public string DisplayTime
        {
            get
            {
                // Show minutes if: realtime information available, time to departure less than 60 minutes
                if (RealtimeSupported && Countdown < 60)
                {
                    return Countdown.ToString();
                }

                // Default
                return String.Format("{0:H:mm}", TimePlanned);
            }
        }
    }
}
