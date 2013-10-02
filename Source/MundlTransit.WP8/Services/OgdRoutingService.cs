using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.Services
{
    public class OgdRoutingService : IRoutingService
    {
        public async Task<RoutingInformation> RetrieveRouteAsync(RoutingRequest request)
        {
            var schnittstelle = new RoutingSchnittstelle();

            RoutingInformation response = await schnittstelle.GetRoutingAsync(request).ConfigureAwait(false);
            return response;
        }
    }
}
