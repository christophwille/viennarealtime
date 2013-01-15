using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DbPrepare.Common
{
    public class OgdDownloader
    {
        public static string DownloadFile(string url)
        {
            var client = new WebClient();

            byte[] rawData = client.DownloadData(url);
            
            var enc = Encoding.GetEncoding("ISO-8859-1");
            return enc.GetString(rawData, 0, rawData.Count());
        }
    }
}
