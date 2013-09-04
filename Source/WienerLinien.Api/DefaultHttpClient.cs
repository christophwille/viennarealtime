using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            try
            {
                var c = new HttpClient();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await c.GetStringAsync(url).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }
    }
}
