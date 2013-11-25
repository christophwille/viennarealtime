using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DbPrepare.Common
{
    public class OgdDownloader
    {
        public static async Task<string> GetAsStringAsync(string url)
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var client = new HttpClient(handler);

            try
            {
                using (var response = await client.GetAsync(url).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return body;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
