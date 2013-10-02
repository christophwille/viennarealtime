using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class LegPoint
    {
        public LegPoint(string date, string time, string name)
        {
            Date = date;
            Time = time;
            Name = name;
        }

        public string Date { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }
    }
}
