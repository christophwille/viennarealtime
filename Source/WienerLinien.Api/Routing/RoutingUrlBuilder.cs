using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api.Routing
{
    public static class RoutingUrlBuilder
    {
        private const string BaseUrl = "http://www.wienerlinien.at/ogd_routing/XML_TRIP_REQUEST2?";

        public static string Build(RoutingRequest request)
        {
            const string urlFormatString = BaseUrl +
                "type_origin=stopID&name_origin={0}&type_destination=stopID&name_destination={1}&ptOptionsActive=1&itOptionsActive=1" +
                "&itdDate={2:yyyyddMM}&idtTime={2:HHmm}&routeType={3}" +
                "&outputFormat=JSON";

            // &itdTripDateTimeDepArr={4}

            var url = String.Format(urlFormatString, 
                request.FromStation, request.ToStation, 
                request.When, RouteTypeToQueryStringParameter(request.RouteType));

            return url;
        }

        private static string RouteTypeToQueryStringParameter(RouteTypeOption option)
        {
            switch (option)
            {
                case RouteTypeOption.LeastTime:
                    return "LEASTTIME";
                    break;
                case RouteTypeOption.LeastInterchange:
                    return "LEASTINTERCHANGE";
                    break;
                case RouteTypeOption.LeastWalking:
                    return "LEASTWALKING";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("option");
            }
        }
    }
}
