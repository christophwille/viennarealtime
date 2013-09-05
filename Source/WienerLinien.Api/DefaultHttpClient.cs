using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public class DefaultHttpClient : IHttpClient
    {
        public async Task<string> GetStringAsync(string url)
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
