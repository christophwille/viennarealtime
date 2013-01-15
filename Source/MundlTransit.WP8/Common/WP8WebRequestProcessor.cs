using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MundlTransit.WP8.Common;
using WienerLinien.Api;

namespace MundlTransit.WP8.Common
{
    public class WP8WebRequestProcessor : IWebRequestProcessor
    {
        public async Task<string> PostXml(string url, string postdata)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = HttpMethod.Post;
                byte[] byteArray = Encoding.UTF8.GetBytes(postdata);
                request.ContentType = "text/xml";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = await request.GetRequestStreamAsync())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                HttpWebResponse response = await request.GetResponseAsync();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async Task<string> Get(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = HttpMethod.Get;

                HttpWebResponse response = await request.GetResponseAsync();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
