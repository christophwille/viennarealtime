using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WienerLinien.Api;

namespace MundlTransit.WP8.Services
{
    public class DefaultMonitorErrorToErrorMessageService
    {

        //        RblNotSpecified,
        //DownloadingFailed,
        //ResponseParsingFailed,
        //MonitorsEmpty,

        //ServerDatabaseUnavailable = 311, // DB nicht verfügbar 
        //ServerStopDoesNotExist = 312, // Haltepunkt existiert nicht 
        //ServerCallQuotaExceeded = 316, // max. Anfragen überschritten 
        //ServerAuthenticationFailed = 317, // Sender existiert nicht 
        //ServerQueryStringParameterInvalid = 320, // GET Anfrage Parameter invalid
        //ServerNoDataInDatabase = 322, // keine Daten in der DB vorhanden

        private const string GenericError = "Realtime data could not be downloaded, please try again later.";
        private const string MonitorsEmpty = "No departures available at this time.";

        public string GetMessage(MonitorInformationErrorCode errCode)
        {
            switch (errCode)
            {
                case MonitorInformationErrorCode.MonitorsEmpty:
                    return MonitorsEmpty;
                    break;
                default:
                    return GenericError;
                    break;
            }
        }
    }
}
