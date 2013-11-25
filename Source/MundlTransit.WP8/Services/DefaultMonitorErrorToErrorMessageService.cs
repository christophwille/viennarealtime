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
                case MonitorInformationErrorCode.DownloadingFailed:
                    return AppResources.MonitorError_DownloadingFailed;
                case MonitorInformationErrorCode.ResponseParsingFailed:
                    return AppResources.MonitorError_ResponseParsingFailed;
                case MonitorInformationErrorCode.MonitorsEmpty:
                    return AppResources.MonitorError_MonitorsEmpty;
                case MonitorInformationErrorCode.ServerDatabaseUnavailable:
                    return AppResources.MonitorError_ServerDatabaseUnavailable;
                case MonitorInformationErrorCode.ServerStopDoesNotExist:
                    return AppResources.MonitorError_ServerStopDoesNotExist;
                case MonitorInformationErrorCode.ServerCallQuotaExceeded:
                    return AppResources.MonitorError_ServerCallQuotaExceeded;
                case MonitorInformationErrorCode.ServerAuthenticationFailed:
                    return AppResources.MonitorError_ServerAuthenticationFailed;
                case MonitorInformationErrorCode.ServerQueryStringParameterInvalid:
                    return AppResources.MonitorError_ServerQueryStringParameterInvalid;
                case MonitorInformationErrorCode.ServerNoDataInDatabase:
                    return AppResources.MonitorError_ServerNoDataInDatabase;
                default:
                    return AppResources.MonitorError_GenericError;
            }
        }
    }
}
