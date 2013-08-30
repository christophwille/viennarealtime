using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public interface ILineInformation
    {
        string Name { get; set; }
        string Towards { get; set; }
        MonitorLineType Type { get; set; }
        bool RealtimeSupported { get; set; }
        bool BarrierFree { get; set; }
    }
}
