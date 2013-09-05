using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using WienerLinien.Api;

namespace MundlTransit.WP8.Common
{
    public class SocketStreamHttpClient : IHttpClient
    {
        public async Task<string> GetStringAsync(string url)
        {
#if DEBUG
            var stopWatch = new Stopwatch();
            stopWatch.Start();
#endif

            try
            {
                var uri = new Uri(url);
                string response = null;

                using (var socket = new StreamSocket())
                {
                    var host = new HostName(uri.Host);

                    const string requestTemplate = "GET {0} HTTP/1.1\r\n" +
                                           "Host: {1}\r\n" +
                                           "Content-Type: application/json\r\n" +
                                           "\r\n";
                    var request = String.Format(requestTemplate, uri.AbsoluteUri, uri.Host);

                    await socket.ConnectAsync(host, "80").AsTask().ConfigureAwait(false);
                    
                    var outStream = socket.OutputStream.AsStreamForWrite();
                    using (var sw = new StreamWriter(outStream))
                    {
                        await sw.WriteAsync(request).ConfigureAwait(false);
                        await sw.FlushAsync().ConfigureAwait(false);
                    }
#if DEBUG
                    Debug.WriteLine("done sending request " + stopWatch.ElapsedMilliseconds);
#endif
                    var inStream = socket.InputStream.AsStreamForRead();

                    using (var reader = new StreamReader(inStream))
                    {
                        response = await reader.ReadToEndAsync().ConfigureAwait(false);
                    }

                    /* http://julien.dollon.net/post/Use-sockets-to-request-a-webpage-with-WinRT.aspx */

                    //var reader = new DataReader(socket.InputStream);
                    //reader.InputStreamOptions = InputStreamOptions.Partial;
                    //var writer = new DataWriter(socket.OutputStream);

                    //await socket.ConnectAsync(host, "80").AsTask().ConfigureAwait(false);
                    //writer.WriteString(request);
                    //await writer.StoreAsync().AsTask().ConfigureAwait(false);

                    //Debug.WriteLine("done sending request " + stopWatch.ElapsedMilliseconds);

                    //uint buffer = 4096;
                    //var count = await reader.LoadAsync(buffer).AsTask().ConfigureAwait(false);

                    //var stb = new StringBuilder();

                    //while (count > 0)
                    //{
                    //    string currentLine = reader.ReadString(count);
                    //    stb.Append(currentLine);
                    //    count = await reader.LoadAsync(buffer).AsTask().ConfigureAwait(false);
                    //}

                    //response = stb.ToString();
#if DEBUG
                    Debug.WriteLine("done getting response " + stopWatch.ElapsedMilliseconds);
#endif
                }
#if DEBUG
                Debug.WriteLine("done all " + stopWatch.ElapsedMilliseconds);
#endif

                return PickResponseBody(response).ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return null;
        }

        StringBuilder PickResponseBody(string response)
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

            return stb;
        }
    }
}
