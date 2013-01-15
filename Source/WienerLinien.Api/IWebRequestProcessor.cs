using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public interface IWebRequestProcessor
    {
        Task<string> PostXml(string url, string postdata);
        Task<string> Get(string url);
    }
}
