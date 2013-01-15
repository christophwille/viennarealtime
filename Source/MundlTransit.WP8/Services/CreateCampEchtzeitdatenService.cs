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
using WienerLinien.Api.Qando;

namespace MundlTransit.WP8.Services
{
    public class CreateCampEchtzeitdatenService : IEchtzeitdatenService
    {
        public async Task<List<MonitorLine>> RetrieveMonitorInformation(Haltestelle haltestelle)
        {
            var schnittstelle = new WienerLinien.Api.CreateCamp.EchtzeitdatenSchnittstelle();

            var punkteSplit = haltestelle.HaltepunkteIds.Split(new char[] { ',' });
            var punkte = punkteSplit.Select(p => Int32.Parse(p)).ToList();

            var processor = new WP8WebRequestProcessor();
            var all = new List<MonitorLine>();

            // This is intentionally not parallel because we are using a non-final API
            foreach (int haltepunkt in punkte)
            {
                var information = await schnittstelle.GetMonitorInformation(haltepunkt, processor);

                if (information.Succeeded)
                    all.AddRange(information.Lines);
            }

            return all;
        }
    }
}
