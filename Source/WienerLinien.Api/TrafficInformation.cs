using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public class TrafficInformation
    {
        public TrafficInformation(bool succeeded=false)
        {
            Succeeded = succeeded;
            Items = new List<TrafficInformationItem>();
        }

        public TrafficInformation(List<TrafficInformationItem> items)
        {
            Succeeded = true;
            Items = items;
        }

        public bool Succeeded { get; set; }
        public List<TrafficInformationItem> Items { get; set; }
    }
}
