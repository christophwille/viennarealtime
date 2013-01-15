using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.CreateCamp
{
    public class CreateCampMonitorLine : MonitorLine
    {
        public List<FahrplanAnlage> Anlagen { get; set; }

        public override string FormatDepartureTimes()
        {
            if (null == Anlagen) return "";

            StringBuilder stb = new StringBuilder();
            bool first = true;

            var now = DateTime.Now;

            foreach (var d in Anlagen)
            {
                if (d.Realtime.HasValue)
                {
                    if (!first) stb.Append(", ");
                    stb.Append((d.Realtime.Value - now).Minutes);
                    first = false;
                }
            }

            return stb.ToString();
        }
    }
}
