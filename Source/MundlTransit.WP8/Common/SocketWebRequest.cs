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

                    await socket.ConnectAsync(host, "80").AsTask().ConfigureAwait(false);

                    const string request = "GET {0} HTTP/1.1\r\n" +
                                           "Host: www.wienerlinien.at\r\n" +
                                           "Content-Type: application/json\r\n" +
                                           "\r\n";

                    var outStream = socket.OutputStream.AsStreamForWrite();
                    using (var sw = new StreamWriter(outStream))
                    {
                        await sw.WriteAsync(String.Format(request, url)).ConfigureAwait(false);
                        await sw.FlushAsync();
                    }

                    var inStream = socket.InputStream.AsStreamForRead();
                    string response = "";
                    using (var reader = new StreamReader(inStream))
                    {
                        response = await reader.ReadToEndAsync().ConfigureAwait(false);
                    }

                    var parts = response.Trim().Split(new string[] {"\r\n"}, StringSplitOptions.None);
                    
                    // This is totally a hack, do not copy/paste as it will break everywhere but this special case
                    var stb = new StringBuilder();
                    bool endOfHeaders = false, appendLine = false;
                    foreach (var p in parts)
                    {
                        if (String.IsNullOrWhiteSpace(p))
                        {
                            endOfHeaders = true;
                            continue;
                        }

                        if (endOfHeaders)
                        {
                            endOfHeaders = false;
                            appendLine = true;
                            continue;
                        }

                        if (appendLine)
                        {
                            if (p.Length > 1)
                                stb.AppendLine(p);
                        }
                    }

                    return stb.ToString();
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
