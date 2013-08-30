namespace WienerLinien.Api
{
    public enum MonitorInformationErrorCode
    {
        RblNotSpecified,
        DownloadingFailed,
        ResponseParsingFailed,
        MonitorsEmpty,

        ServerDatabaseUnavailable = 311, // DB nicht verfügbar 
        ServerStopDoesNotExist = 312, // Haltepunkt existiert nicht 
        ServerCallQuotaExceeded = 316, // max. Anfragen überschritten 
        ServerAuthenticationFailed = 317, // Sender existiert nicht 
        ServerQueryStringParameterInvalid = 320, // GET Anfrage Parameter invalid
        ServerNoDataInDatabase = 322, // keine Daten in der DB vorhanden
    }
}