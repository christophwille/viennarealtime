using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using WienerLinien.Api;

namespace MundlTransit.WP8.Services
{
    public interface IEchtzeitdatenService
    {
        Task<List<MonitorLine>> RetrieveMonitorInformation(Haltestelle haltestelle);
    }
}
