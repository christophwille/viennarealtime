using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api;

namespace MundlTransit.WP8.Common
{
    // DO NOT USE, DOES NOT WORK WITH CONTENT-TYPE HEADER !!!
    public class AsyncWebRequest : IAsyncWebRequest
    {
        public async Task<string> Get(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = HttpMethod.Get;
                request.ContentType = "application/json";       // Bombs with a protocol violation

                HttpWebResponse response = await request.GetResponseAsync();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
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
