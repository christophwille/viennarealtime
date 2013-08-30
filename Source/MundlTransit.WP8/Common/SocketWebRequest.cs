using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using WienerLinien.Api;


namespace MundlTransit.WP8.Common
{
    public class SocketWebRequest : IAsyncWebRequest
    {
        public async Task<string> Get(string url)
        {
            try
            {
                using (var socket = new StreamSocket())
                {
                    var host = new HostName("www.wienerlinien.at");

                    await socket.ConnectAsync(host, "80");

                    const string request = "GET {0} HTTP/1.1\r\n" +
                                           "Host: www.wienerlinien.at\r\n" +
                                           "Content-Type: application/json\r\n" +
                                           "\r\n";

                    var outStream = socket.OutputStream.AsStreamForWrite();
                    using (var sw = new StreamWriter(outStream))
                    {
                        await sw.WriteAsync(String.Format(request, url));
                        await sw.FlushAsync();
                    }

                    var inStream = socket.InputStream.AsStreamForRead();
                    string response = "";
                    using (var reader = new StreamReader(inStream))
                    {
                        response = await reader.ReadToEndAsync();
                    }

                    var parts = response.Trim().Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    var length = parts.Length;

                    if (length < 2) return "";

                    return parts[length - 2];
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
