using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api.Routing.Messages;

namespace WienerLinien.Api.Routing
{
    public class RoutingSchnittstelle
    {
        public bool ParseRoutingRequestResponse(string response)
        {
            try
            {
                var request = MessageSerializationHelper.DeserializeFromString<itdRequestType>(response);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
