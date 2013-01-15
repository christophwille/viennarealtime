using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public enum MonitorInformationErrorCode
    {
        DownloadingFailed,
        ResponseParsingFailed

    }
    public class MonitorInformation
    {
        public MonitorInformation(List<MonitorLine> lines)
        {
            Lines = lines;
            Succeeded = true;
        }

        public MonitorInformation(MonitorInformationErrorCode errorCode)
        {
            ErrorCode = errorCode;
            Succeeded = false;
        }

        public bool Succeeded { get; set; }
        public MonitorInformationErrorCode ErrorCode { get; set; }

        public List<MonitorLine> Lines { get; set; }
    }
}
