using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WienerLinien.Api.Ogd
{
    public class EchtzeitdatenSchnittstelle : IEchtzeitdatenSchnittstelle
    {
        // {0}: the multiple rbls
        // {1}: the Api key
        // {2}: this is a "no-cache" parameter (Ticks)
        private const string ApiUrl = "http://www.wienerlinien.at/ogd_realtime/monitor?{0}&sender={1}&vrtnocache={2}";

        private string _apiKey;

        public void InitializeApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<MonitorInformation> GetMonitorInformation(List<int> rblList) // , IAsyncWebRequest requestor)
        {
            if (null == rblList || !rblList.Any())
            {
                return new MonitorInformation(MonitorInformationErrorCode.RblNotSpecified);
            }

            var rbls = String.Join("&", rblList.Select(rbl => String.Format("rbl={0}", rbl)));
            var url = String.Format(ApiUrl, rbls, _apiKey, DateTime.Now.Ticks.ToString());

            string response = null;
            try
            {
                var c = new HttpClient();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await c.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            // var response = await requestor.Get(url); // use this with IAsyncWebRequest

            if (null == response)
                new MonitorInformation(MonitorInformationErrorCode.DownloadingFailed);

            try
            {
                return ParseMonitorResponse(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return
                new MonitorInformation(MonitorInformationErrorCode.ResponseParsingFailed);
        }


        public MonitorInformation ParseMonitorResponse(string jsonResponse)
        {
            var rootObj = JsonConvert.DeserializeObject<RootObject>(jsonResponse);

            if (null == rootObj)
            {
                return new MonitorInformation(MonitorInformationErrorCode.ResponseParsingFailed);
            }

            // Check response error codes first (add to ErrorCode Collection with same # as in actual return values
            if (null == rootObj.data)
            {
                if (null != rootObj.message)
                {
                    var errCode = MonitorInformationErrorCode.ResponseParsingFailed;
                    bool parseOk = Enum.TryParse<MonitorInformationErrorCode>(rootObj.message.messageCode.ToString(), out errCode);

                    return new MonitorInformation(errCode);
                }
                else
                {
                    return new MonitorInformation(MonitorInformationErrorCode.ResponseParsingFailed);
                }
            }

            // Is there anything at all?
            if (!rootObj.data.monitors.Any())
            {
                return new MonitorInformation(MonitorInformationErrorCode.MonitorsEmpty);
            }

            // Parse monitor information

            var parsedMonitorLines = new List<MonitorLine>();

            foreach (var monitor in rootObj.data.monitors)
            {
                foreach (var line in monitor.lines)
                {
                    var ml = new MonitorLine()
                    {
                        Name = line.name,
                        Towards = line.towards,
                        Type = MonitorLineTypeMapper.TypeStringToType(line.type),
                        RealtimeSupported = line.realtimeSupported,
                        BarrierFree = line.barrierFree,
                        Departures = new List<Api.Departure>()
                    };

                    if (null != line.departures && null != line.departures.departure)
                    {
                        foreach (var departure in line.departures.departure)
                        {
                            var timePlanned = ToLocalTime(departure.departureTime.timePlanned);
                            var timeReal = ToLocalTime(departure.departureTime.timeReal);

                            var md = new Api.Departure()
                            {
                                Name = ml.Name,
                                Towards = ml.Towards,
                                Type = ml.Type,
                                RealtimeSupported = ml.RealtimeSupported,
                                BarrierFree = ml.BarrierFree,
                                Countdown = departure.departureTime.countdown,
                                TimeReal = timeReal,
                                TimePlanned = timePlanned
                            };

                            // Override line defaults for this departure if necessary
                            if (null != departure.vehicle)
                            {
                                md.OverridesLineInformation = true;
                                md.Name = departure.vehicle.name;
                                md.Type = MonitorLineTypeMapper.TypeStringToType(departure.vehicle.type);
                                md.RealtimeSupported = departure.vehicle.realtimeSupported;
                                md.BarrierFree = departure.vehicle.barrierFree;
                            }

                            ml.Departures.Add(md);
                        }
                    }

                    parsedMonitorLines.Add(ml);
                }
            }

            var orderForReturn = parsedMonitorLines.OrderBy(moli => moli.Type).ThenBy(moli => moli.Name).ThenBy(moli => moli.Towards);
            return new MonitorInformation(orderForReturn.ToList());
        }

        private DateTime? ToLocalTime(string jsonDatetime)
        {
            DateTime parsed;
            bool ok = DateTime.TryParse(jsonDatetime, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);

            if (ok)
                return parsed.ToLocalTime();

            return null;
        }
    }
}
