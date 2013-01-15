using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public class MonitorLine
    {
        public string Name { get; set; }
        public string Towards { get; set; }
        public MonitorLineType Type { get; set; }

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
            throw new NotImplementedException();
        }

        public string DepartureTimes
        {
            get { return FormatDepartureTimes(); }
            set { }
        }
    }
}
