using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RP = WienerLinien.Api.Routing.RoutingProxies;

namespace WienerLinien.Api.Routing
{
    public class RoutingSchnittstelle
    {
        public const string BaseUrl = "http://www.wienerlinien.at/ogd_routing/XML_TRIP_REQUEST2?";
        public async Task<RoutingInformation> GetRoutingAsync(RoutingRequest request)
        {
            const string urlFormatString = BaseUrl + 
                "type_origin=stopID&name_origin={0}&type_destination=stopID&name_destination={1}&ptOptionsActive=1&outputFormat=JSON";

            var url = String.Format(urlFormatString, request.FromStation, request.ToStation);
            var client = new DefaultHttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);

            if (null == response)
            {
                return new RoutingInformation(RoutingInformationErrorCode.DownloadingFailed);
            }

            try
            {
                return ParseRoutingRequestResponse(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return
                new RoutingInformation(RoutingInformationErrorCode.ResponseParsingFailed);
        }

        public RoutingInformation ParseRoutingRequestResponse(string jsonResponse)
        {
            var rootObj = JsonConvert.DeserializeObject<RP.RootObject>(jsonResponse);
            return new RoutingInformation(RoutingInformationErrorCode.ResponseParsingFailed);
        }
    }
}
