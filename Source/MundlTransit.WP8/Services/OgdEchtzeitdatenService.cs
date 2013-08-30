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

        public async Task<MonitorInformation> RetrieveMonitorInformation(Haltestelle haltestelle)
        {
            if (String.IsNullOrWhiteSpace(haltestelle.RblNummern))
                return null;

            // turn comma-separated string into List of int32
            var rbls = haltestelle.RblNummern
                .Split(new char[] {','})
                .Select(Int32.Parse)
                .ToList();

            var schnittstelle = new WienerLinien.Api.Ogd.EchtzeitdatenSchnittstelle();
            schnittstelle.InitializeApi(_apiKey);

            MonitorInformation response = await schnittstelle.GetMonitorInformation(rbls, new SocketWebRequest());

            return response;
        }
    }
}
