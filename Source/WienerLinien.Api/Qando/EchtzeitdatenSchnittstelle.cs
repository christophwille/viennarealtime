using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WienerLinien.Api.Qando
{
    public class EchtzeitdatenSchnittstelle : IEchtzeitdatenSchnittstelle
    {
        private const string XmlRequest =
            @"<?xml version=""1.0"" encoding=""UTF-8""?> 
                <ft> 
                    <request clientId=""123"" apiName=""api_get_monitor"" apiVersion=""2.0""> 
                        <client clientId=""123""/> 
                        <requestType>api_get_monitor</requestType> 
                        <monitor> 
                            <outputCoords>WGS84</outputCoords> 
                            <type>stop</type> 
                            <name>6020{0}</name> 
                            <year>{1}</year> 
                            <month>{2}</month> 
                            <day>{3}</day> 
                            <hour>{4}</hour> 
                            <minute>{5}</minute> 
                            <line></line> 
                            <sourceFrom>stoplist</sourceFrom> 
                        </monitor> 
                    </request> 
                </ft>";

        private const string ApiUrl = "http://webservice.qando.at/2.0/webservice.ft";

        public async Task<MonitorInformation> GetMonitorInformation(int haltestellenId, IWebRequestProcessor webRequestProcessor)
        {
            var postdata = GetMonitorRequest(haltestellenId, DateTime.Now);

            string response = await webRequestProcessor.PostXml(ApiUrl, postdata);
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

        public string GetMonitorRequest(int haltestellenId, DateTime forTime)
        {
            string paddedHaltestellenId = String.Format("{0:0000}", haltestellenId);

            return String.Format(XmlRequest,
                                 paddedHaltestellenId,
                                 forTime.Year, forTime.Month, forTime.Day,
                                 forTime.Hour, forTime.Minute);
        }

        public MonitorInformation ParseMonitorResponse(string xml)
        {
            var root = XElement.Parse(xml);

            var responseElement = root.Elements("response").FirstOrDefault();
            var monitorElement = responseElement.Elements("monitor").FirstOrDefault();
            var linesElement = monitorElement.Elements("lines").FirstOrDefault();

            // TEST
            string linesCount = (string)linesElement.Attribute("count");

            var lineList = new List<MonitorLine>();
            foreach (var line in linesElement.Elements("line"))
            {
                var l = ParseLineElement(line);

                if (null != l)
                    lineList.Add(l);
            }

            // TEST
            var s = lineList.Count;

            var response = new MonitorInformation(lineList);
            return response;
        }

        private MonitorLine ParseLineElement(XElement line)
        {
            string name = (string)line.Attribute("name");
            string type = (string)line.Attribute("type");
            string towards = (string)line.Attribute("towards");

            string realtimeSupported = (string)line.Attribute("realtimeSupported");
            bool isRealtimeSupported = (!String.IsNullOrWhiteSpace(realtimeSupported) && "1" == realtimeSupported);

            var departuresElement = line.Elements("departures").FirstOrDefault();
            var departuresList = ParseDeparturesElement(departuresElement);

            if (null == departuresList || !departuresList.Any()) return null;

            var l = new QandoMonitorLine()
            {
                Name = name,
                Towards = towards,
                Type = TypeStringToType(type),
                Departures = departuresList,
                RealtimeSupported = isRealtimeSupported
            };

            return l;
        }

        private List<QandoDeparture> ParseDeparturesElement(XElement departuresElement)
        {
            var departuresList = new List<QandoDeparture>();

            foreach (var departure in departuresElement.Elements("departure"))
            {
                var departureTimeElement = departure.Elements("departureTime").FirstOrDefault();

                string countdown = (string)departureTimeElement.Attribute("countdown");
                int? countdownConverted = null;
                if (!String.IsNullOrWhiteSpace(countdown)) countdownConverted = Int32.Parse(countdown);

                string timePlanned = (string)departureTimeElement.Attribute("timePlanned");
                DateTime? timePlannedConverted = null;
                if (!String.IsNullOrWhiteSpace(timePlanned)) timePlannedConverted = DateTime.Parse(timePlanned);

                string timeReal = (string)departureTimeElement.Attribute("timeReal");
                DateTime? timeRealConverted = null;
                if (!String.IsNullOrWhiteSpace(timeReal)) timeRealConverted = DateTime.Parse(timeReal);

                // TODO: implement delay Attribute

                // "NICHT EINSTEIGEN !" has neither of those three fields set
                if (timePlannedConverted.HasValue || timeRealConverted.HasValue || countdownConverted.HasValue)
                {
                    var d = new QandoDeparture()
                                {
                                    Countdown = countdownConverted,
                                    TimePlanned = timePlannedConverted,
                                    TimeReal = timeRealConverted
                                };

                    departuresList.Add(d);
                }
            }

            return departuresList;
        }

        public MonitorLineType TypeStringToType(string type)
        {
            var retType = MonitorLineType.Unknown;

            switch (type.ToLowerInvariant())
            {
                case "ptmetro":
                    retType = MonitorLineType.Metro;
                    break;
                case "pttram":
                    retType = MonitorLineType.Tram;
                    break;
                case "ptbuscity":
                    retType = MonitorLineType.Bus;
                    break;
                case "pttrains":
                    retType = MonitorLineType.SBahn;
                    break;
                case "pttrainr":
                    retType = MonitorLineType.Regionalzug;
                    break;
                case "other":
                    retType = MonitorLineType.Other;
                    break;
                    
            }

            return retType;
        }
    }
}
