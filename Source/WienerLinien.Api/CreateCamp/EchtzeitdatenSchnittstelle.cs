using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WienerLinien.Api.CreateCamp
{
    public class EchtzeitdatenSchnittstelle : IEchtzeitdatenSchnittstelle
    {
        private const string ApiUrl = "http://ogd-createcamp-wienerlinien.at/webservice.ft/getMonitorXml?haltepunkt={0}&sender=createcamp20";

        public async Task<MonitorInformation> GetMonitorInformation(int haltepunktId, IWebRequestProcessor webRequestProcessor)
        {
            var url = String.Format(ApiUrl, haltepunktId);

            string response = await webRequestProcessor.Get(url);
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

        public MonitorInformation ParseMonitorResponse(string xml)
        {
            var root = XElement.Parse(xml);

            var nachrichtElement = root.Elements("AZBNachricht").FirstOrDefault();

            var listOfAnlagen = new List<FahrplanAnlage>();
            foreach (var anlage in nachrichtElement.Elements("AZBFahrplanlage"))
            {
                var linienTextElement = (string)anlage.Elements("LinienText").FirstOrDefault();
                var richtungsTextElement = (string)anlage.Elements("RichtungsText").FirstOrDefault();
                var prognoseZeit = (string)anlage.Elements("AnkunftszeitAZBPrognose").FirstOrDefault();

                string ubahnFix = (string)anlage.Elements("AbfahrtszeitAZBPrognose").FirstOrDefault();

                DateTime? time = null;
                if (!String.IsNullOrWhiteSpace(prognoseZeit)) time = DateTime.Parse(prognoseZeit);
                if (null == time && !String.IsNullOrWhiteSpace(ubahnFix)) time = DateTime.Parse(ubahnFix);

                var linienId = (string)anlage.Elements("LinienID").FirstOrDefault();

                listOfAnlagen.Add(new FahrplanAnlage()
                                      {
                                          Line = linienTextElement,
                                          Towards = richtungsTextElement,
                                          Realtime = time,
                                          Type = LineNumberToType(linienId)
                                      });
            }

            var query = from a in listOfAnlagen
                        group a by new { a.Line, a.Towards }
                            into g
                            select new
                            {
                                Group = g.Key,
                                Anlagen = g.ToList()
                            };

            var lineList = new List<MonitorLine>();
            foreach (var groupedAnlage in query)
            {
                var firstAnlage = groupedAnlage.Anlagen.First();

                var line = new CreateCampMonitorLine()
                               {
                                   Name = firstAnlage.Line,
                                   Towards = firstAnlage.Towards,
                                   Type = firstAnlage.Type
                               };

                line.Anlagen = groupedAnlage.Anlagen
                    .OrderBy(t => t.Realtime)
                    .ToList();

                lineList.Add(line);
            }

            return new MonitorInformation(lineList);
        }

        // zB <LinienID>415</LinienID>
        // 2 B Bus, 4 A Bus, 3 U Bahn, 1 Bim, 3 U Bahn, 399 WLB, 5 Nachtbus
        public MonitorLineType LineNumberToType(string lineNumber)
        {
            char startsWith = lineNumber[0];
            var retType = MonitorLineType.Unknown;

            switch (startsWith)
            {
                case '3':
                    retType = MonitorLineType.Metro;
                    break;
                case '1':
                    retType = MonitorLineType.Tram;
                    break;
                case '2':
                    retType = MonitorLineType.BusB;
                    break;
                case '4':
                    retType = MonitorLineType.Bus;
                    break;
                case '5':
                    retType = MonitorLineType.NightBus;
                    break;
            }

            return retType;
        }
    }
}
