using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using WienerLinien.Api;

namespace MundlTransit.WP8.Common
{
    public class SocketStreamHttpClient : IHttpClient
    {
        public async Task<string> GetStringAsync(string url)
        {
            try
            {
                var uri = new Uri(url);

                using (var socket = new StreamSocket())
                {
                    var host = new HostName(uri.Host);

                    await socket.ConnectAsync(host, "80").AsTask().ConfigureAwait(false);

                    const string request = "GET {0} HTTP/1.1\r\n" +
                                           "Host: {1}\r\n" +
                                           "Content-Type: application/json\r\n" +
                                           "\r\n";

                    var outStream = socket.OutputStream.AsStreamForWrite();
                    using (var sw = new StreamWriter(outStream))
                    {
                        var requestToSend = String.Format(request, uri.AbsoluteUri, uri.Host);
                        await sw.WriteAsync(requestToSend).ConfigureAwait(false);
                        await sw.FlushAsync().ConfigureAwait(false);
                    }

                    var inStream = socket.InputStream.AsStreamForRead();

                    using (var reader = new StreamReader(inStream))
                    {
                        var response = await reader.ReadToEndAsync().ConfigureAwait(false);
                        return PickResponseBody(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }

        string PickResponseBody(string response)
        {
            var parts = response.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);

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
}
