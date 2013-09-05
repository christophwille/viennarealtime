using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using Newtonsoft.Json.Linq;
using WienerLinien.Api;

namespace MundlTransit.WP8.Services
{
    public class OgdEchtzeitdatenService : IEchtzeitdatenService
    {
        private readonly string _apiKey;
        public OgdEchtzeitdatenService(IConfigurationService config)
        {
            _apiKey = config.WienerLinienApiKey;
        }

        public async Task<MonitorInformation> RetrieveMonitorInformationAsync(Haltestelle haltestelle)
        {
            var rbls = RblNummernStringToIntList(haltestelle.RblNummern);

            if (!rbls.Any())
                return new MonitorInformation(MonitorInformationErrorCode.RblNotSpecified);

            var schnittstelle = new WienerLinien.Api.Ogd.EchtzeitdatenSchnittstelle();
            schnittstelle.InitializeApi(_apiKey);

            IHttpClient client = null;
            // client = new SocketStreamHttpClient();

            MonitorInformation response = await schnittstelle.GetMonitorInformationAsync(rbls, client).ConfigureAwait(false);

            return response;
        }

        private List<int> RblNummernStringToIntList(string rblnummern)
        {
            if (String.IsNullOrWhiteSpace(rblnummern)) 
                return new List<int>();

            // Turn comma-separated string into List of int32
            var rblsToParse = rblnummern
                .Split(new char[] { ',' });

            var rbls = new List<int>();

            foreach (var rblToParse in rblsToParse)
            {
                int rbl = 0;
                if (Int32.TryParse(rblToParse, out rbl))
                    rbls.Add(rbl);
            }

            return rbls;
        }

        public async Task<TrafficInformation> RetrieveTrafficInfoListAsync()
        {
            var schnittstelle = new WienerLinien.Api.Ogd.EchtzeitdatenSchnittstelle();
            schnittstelle.InitializeApi(_apiKey);

            TrafficInformation response = await schnittstelle.GetTrafficInfoListAsync().ConfigureAwait(false);

            return response;
        }
    }
}
