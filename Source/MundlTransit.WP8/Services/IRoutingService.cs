using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.Services
{
    public interface IRoutingService
    {
        Task<RoutingInformation> RetrieveRouteAsync(RoutingRequest request);
    }
}
