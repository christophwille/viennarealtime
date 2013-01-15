using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Model;

namespace MundlTransit.WP8.Services
{
    public interface ILocationService
    {
        Task<LocationResult> GetCurrentPosition();

        bool? GetCurrentConsentValue();
        bool CheckConsentAskIfNotSet();
        void SetConsentValue(bool value);
    }
}
