using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinien.Api
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
    }
}
