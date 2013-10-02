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
            Debug.WriteLine(url);

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

            if (null == rootObj)
            {
                return new RoutingInformation(RoutingInformationErrorCode.ResponseParsingFailed);
            }

            // Is there anything at all?
            if (!rootObj.trips.Any())
            {
                return new RoutingInformation(RoutingInformationErrorCode.ResponseParsingFailed);
            }

            var rTrips = new List<Trip>();
            int tripNumber = 1;

            foreach (RP.Trip outerTrip in rootObj.trips)
            {
                if (null != outerTrip.trip)
                {
                    RP.Trip2 trip = outerTrip.trip;

                    var duration = new TimeSpan();
                    bool durationParseOk = TimeSpan.TryParse(trip.duration, out duration);
                    bool interchange = 0 == String.Compare("1", trip.interchange, StringComparison.OrdinalIgnoreCase);

                    var rTrip = new Trip(tripNumber++, duration, interchange);

                    foreach (RP.Leg leg in trip.legs)
                    {
                        var tt = RoutingTypeOfTransportation.Walk;

                        if (leg.mode != null && Enum.TryParse(leg.mode.code, out tt))
                        {
                            // For RoutingTypeOfTransportation.Walk, .number ("U3") and .destination ("Ottakring") are empty strings
                            var rLeg = new TripLeg(tt, leg.mode.number, leg.mode.destination);

                            foreach (RP.Point point in leg.points)
                            {
                                var rLegPoint = new LegPoint(point.dateTime.date, point.dateTime.time, point.name);
                                if (0 == String.Compare("arrival", point.usage, StringComparison.OrdinalIgnoreCase))
                                {
                                    rLeg.Arrival = rLegPoint;
                                }
                                else
                                {
                                    rLeg.Departure = rLegPoint;
                                }
                            }

                            if (rLeg.Arrival != null && rLeg.Departure != null)
                            {
                                rTrip.Legs.Add(rLeg);
                            }
                        }
                    }

                    if (rTrip.Legs.Any())
                    {
                        rTrips.Add(rTrip);
                    }
                }
            }

            return new RoutingInformation(rTrips);
        }
    }
}
