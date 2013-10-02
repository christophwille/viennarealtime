using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RP = WienerLinien.Api.Routing.RoutingProxies;

namespace WienerLinien.Api.Routing
{
    public class RoutingSchnittstelle
    {
        public bool ParseRoutingRequestResponse(string jsonResponse)
        {
            var rootObj = JsonConvert.DeserializeObject<RP.RootObject>(jsonResponse);

            return true;
        }
    }
}
