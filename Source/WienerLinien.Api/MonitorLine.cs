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

        public List<Departure> DeparturesDisplay
        {
            get
            {
                return Departures.Take(4).ToList();
            }
        }

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

                stb.Append(d.DisplayTime);

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
