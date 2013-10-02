using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public class RoutingInformation
    {
        public RoutingInformation(RoutingInformationErrorCode errorCode)
        {
            ErrorCode = errorCode;
            Succeeded = false;
        }

        public bool Succeeded { get; set; }
        public RoutingInformationErrorCode ErrorCode { get; set; }
    }
}
