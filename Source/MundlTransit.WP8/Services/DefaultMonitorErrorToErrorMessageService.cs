using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Resources;
using WienerLinien.Api;

namespace MundlTransit.WP8.Services
{
    public class DefaultMonitorErrorToErrorMessageService
    {
        public string GetMessage(MonitorInformationErrorCode errCode)
        {
            switch (errCode)
            {
                case MonitorInformationErrorCode.RblNotSpecified:
                    return AppResources.MonitorError_RblNotSpecified;
                    break;
                case MonitorInformationErrorCode.DownloadingFailed:
                    return AppResources.MonitorError_DownloadingFailed;
                    break;
                case MonitorInformationErrorCode.ResponseParsingFailed:
                    return AppResources.MonitorError_ResponseParsingFailed;
                    break;
                case MonitorInformationErrorCode.MonitorsEmpty:
                    return AppResources.MonitorError_MonitorsEmpty;
                    break;
                case MonitorInformationErrorCode.ServerDatabaseUnavailable:
                    return AppResources.MonitorError_ServerDatabaseUnavailable;
                    break;
                case MonitorInformationErrorCode.ServerStopDoesNotExist:
                    return AppResources.MonitorError_ServerStopDoesNotExist;
                    break;
                case MonitorInformationErrorCode.ServerCallQuotaExceeded:
                    return AppResources.MonitorError_ServerCallQuotaExceeded;
                    break;
                case MonitorInformationErrorCode.ServerAuthenticationFailed:
                    return AppResources.MonitorError_ServerAuthenticationFailed;
                    break;
                case MonitorInformationErrorCode.ServerQueryStringParameterInvalid:
                    return AppResources.MonitorError_ServerQueryStringParameterInvalid;
                    break;
                case MonitorInformationErrorCode.ServerNoDataInDatabase:
                    return AppResources.MonitorError_ServerNoDataInDatabase;
                    break;
                default:
                    return AppResources.MonitorError_GenericError;
                    break;
            }
        }
    }
}
