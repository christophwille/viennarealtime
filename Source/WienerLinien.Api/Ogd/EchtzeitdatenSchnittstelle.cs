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
using MP = WienerLinien.Api.Ogd.MonitorProxies;
using TILP = WienerLinien.Api.Ogd.TrafficInfoListProxies;

namespace WienerLinien.Api.Ogd
{
    public class EchtzeitdatenSchnittstelle : IEchtzeitdatenSchnittstelle
    {
        // {0}: the multiple rbls
        // {1}: the Api key
        // {2}: this is a "no-cache" parameter (Ticks)
        private const string MonitorApiUrl = "http://www.wienerlinien.at/ogd_realtime/monitor?{0}&sender={1}&vrtnocache={2}";

        // {0}: the Api key
        // {1}: this is a "no-cache" parameter (Ticks)
        private const string TrafficInfoListApiUrl = "http://www.wienerlinien.at/ogd_realtime/trafficInfoList?name=stoerunglang&sender={0}&vrtnocache={1}";

        private string _apiKey;

        public void InitializeApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        private string GenerateVrtNoCacheParameterValue()
        {
            return DateTime.Now.Ticks.ToString();
        }

        public async Task<MonitorInformation> GetMonitorInformationAsync(List<int> rblList, IHttpClient client=null)
        {
            if (null == rblList || !rblList.Any())
            {
                return new MonitorInformation(MonitorInformationErrorCode.RblNotSpecified);
            }

            var rbls = String.Join("&", rblList.Select(rbl => String.Format("rbl={0}", rbl)));
            var url = String.Format(MonitorApiUrl, rbls, _apiKey, GenerateVrtNoCacheParameterValue());

            if (null == client)
                client = new DefaultHttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);

            if (null == response)
            {
                return new MonitorInformation(MonitorInformationErrorCode.DownloadingFailed);
            }

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

        public async Task<TrafficInformation> GetTrafficInfoListAsync()
        {
            var url = String.Format(TrafficInfoListApiUrl, _apiKey, GenerateVrtNoCacheParameterValue());
            var response = await new DefaultHttpClient().GetStringAsync(url).ConfigureAwait(false);

            if (null == response)
                return new TrafficInformation();

            try
            {
                return ParseTrafficInfoListResponse(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return
                new TrafficInformation();
        }


        public MonitorInformation ParseMonitorResponse(string jsonResponse)
        {
            var rootObj = JsonConvert.DeserializeObject<MP.RootObject>(jsonResponse);

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
                            var dt = departure.departureTime;

                            // Empty departureTime object show all values as null or 0
                            if (dt.timePlanned == null && dt.timeReal == null && dt.countdown == 0)
                            {
                                continue;
                            }

                            var timePlanned = ToLocalTime(dt.timePlanned);
                            var timeReal = ToLocalTime(dt.timeReal);

                            var md = new Api.Departure()
                            {
                                Name = ml.Name,
                                Towards = ml.Towards,
                                Type = ml.Type,
                                RealtimeSupported = ml.RealtimeSupported,
                                BarrierFree = ml.BarrierFree,
                                Countdown = dt.countdown,
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

                    // Do not add empty lines (NICHT EINSTEIGEN, ALLE ZÜGE GLEIS 1, &c)
                    if (ml.Departures.Any())
                    {
                        parsedMonitorLines.Add(ml);
                    }
                }
            }

            var orderForReturn = parsedMonitorLines.OrderBy(moli => moli.Type).ThenBy(moli => moli.Name).ThenBy(moli => moli.Towards);
            return new MonitorInformation(orderForReturn.ToList());
        }

        public TrafficInformation ParseTrafficInfoListResponse(string jsonResponse)
        {
            var rootObj = JsonConvert.DeserializeObject<TILP.RootObject>(jsonResponse);

            if (null == rootObj || null == rootObj.data)
            {
                return new TrafficInformation();
            }

            // There is no traffic information at all (i.e. no alerts)
            if (null == rootObj.data.trafficInfos)
            {
                return new TrafficInformation(succeeded: true);
            }

            var items = new List<TrafficInformationItem>();
            foreach (var ti in rootObj.data.trafficInfos)
            {
                var item = new TrafficInformationItem()
                {
                    Title = ti.title,
                    Description = ti.description,
                    RelatedLines = String.Join(", ", ti.relatedLines),
                    Start = ToLocalTime(ti.time.start),
                    End = ToLocalTime(ti.time.end)
                };

                items.Add(item);
            }

            if (items.Any())
                return new TrafficInformation(items);

            return new TrafficInformation(succeeded: true);
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
