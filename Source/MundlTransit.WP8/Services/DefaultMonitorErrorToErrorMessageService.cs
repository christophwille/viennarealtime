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
        //RblNotSpecified,
        //DownloadingFailed,
        //ResponseParsingFailed,
        //MonitorsEmpty,

        //ServerDatabaseUnavailable = 311, // DB nicht verfügbar 
        //ServerStopDoesNotExist = 312, // Haltepunkt existiert nicht 
        //ServerCallQuotaExceeded = 316, // max. Anfragen überschritten 
        //ServerAuthenticationFailed = 317, // Sender existiert nicht 
        //ServerQueryStringParameterInvalid = 320, // GET Anfrage Parameter invalid
        //ServerNoDataInDatabase = 322, // keine Daten in der DB vorhanden

        private const string GenericError = "An unexpected error occured, please try again later.";

        private const string RblNotSpecified = "Internal app error: RBL not specified.";
        private const string DownloadingFailed = "Downloading departure information failed.";
        private const string ResponseParsingFailed = "Parsing the response failed.";
        private const string MonitorsEmpty = "No departures available at this time.";

        private const string ServerDatabaseUnavailable = "Server error reported: Database unavailable.";
        private const string ServerStopDoesNotExist = "This stop does not exist (stop database out of date?).";
        private const string ServerCallQuotaExceeded = "This app has exceeded the call quota allowed by Wiener Linien.";
        private const string ServerAuthenticationFailed = "This app has failed to authenticate to the Wiener Linien API.";
        private const string ServerQueryStringParameterInvalid = "This app has failed to send a properly formatted query to the Wiener Linien API.";
        private const string ServerNoDataInDatabase = "Server error reported: No data in database.";

        public string GetMessage(MonitorInformationErrorCode errCode)
        {
            switch (errCode)
            {
                case MonitorInformationErrorCode.RblNotSpecified:
                    return RblNotSpecified;
                    break;
                case MonitorInformationErrorCode.DownloadingFailed:
                    return DownloadingFailed;
                    break;
                case MonitorInformationErrorCode.ResponseParsingFailed:
                    return ResponseParsingFailed;
                    break;
                case MonitorInformationErrorCode.MonitorsEmpty:
                    return MonitorsEmpty;
                    break;
                case MonitorInformationErrorCode.ServerDatabaseUnavailable:
                    return ServerDatabaseUnavailable;
                    break;
                case MonitorInformationErrorCode.ServerStopDoesNotExist:
                    return ServerStopDoesNotExist;
                    break;
                case MonitorInformationErrorCode.ServerCallQuotaExceeded:
                    return ServerCallQuotaExceeded;
                    break;
                case MonitorInformationErrorCode.ServerAuthenticationFailed:
                    return ServerAuthenticationFailed;
                    break;
                case MonitorInformationErrorCode.ServerQueryStringParameterInvalid:
                    return ServerQueryStringParameterInvalid;
                    break;
                case MonitorInformationErrorCode.ServerNoDataInDatabase:
                    return ServerNoDataInDatabase;
                    break;
                default:
                    return GenericError;
                    break;
            }
        }
    }
}
