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
    public class QandoEchtzeitdatenService : IEchtzeitdatenService
    {
        // A Gzip Http Web Request Sample that can come in handy
        // http://www.sharpgis.net/post/2011/08/28/GZIP-Compressed-Web-Requests-in-WP7-Take-2.aspx
        public void TestGzipClient()
        {
            var uri = new Uri("http://www.somewhere.com/");

            WebClient client = new SharpGIS.GZipWebClient();
            client.DownloadStringCompleted += (sender, args) =>
            {
                // if (null != eventArgs.Error)
                // http://www.west-wind.com/weblog/posts/2012/Aug/30/Using-JSONNET-for-dynamic-JSON-parsing
            };

            client.DownloadStringAsync(uri);
        }


        public async Task<List<MonitorLine>> RetrieveMonitorInformation(Haltestelle haltestelle)
        {
            var schnittstelle = new WienerLinien.Api.Qando.EchtzeitdatenSchnittstelle();

            var response = await schnittstelle.GetMonitorInformation(
                haltestelle.Id,
                new WP8WebRequestProcessor());

            return response.Succeeded ? response.Lines : null;
        }
    }
}
