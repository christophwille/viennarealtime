using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Windows.Foundation;

namespace MundlTransit.WP8
{
    public class AssociationUriMapper : UriMapperBase
    {
        // VIEnna-PublicTransport
        private const string ProtocolName = "vie-pt";
        private const string ProtocolQueryStringParameterName = "encodedLaunchUri=";
        private const string NavigateToDepartures = "Departures";
        private const string StationIdParameterName = "StationId";

        public override Uri MapUri(Uri uri)
        {
            string decodedUri = HttpUtility.UrlDecode(uri.ToString());
            int idxOfLaunchUri = decodedUri.IndexOf(ProtocolQueryStringParameterName, StringComparison.InvariantCultureIgnoreCase);

            try
            {
                if (-1 != idxOfLaunchUri)
                {
                    string launchUriFromQueryString = decodedUri.Substring(idxOfLaunchUri + ProtocolQueryStringParameterName.Length);
                    var launchUri = new Uri(launchUriFromQueryString);

                    // BUG: http://social.msdn.microsoft.com/Forums/wpapps/en-US/86b2cdbb-e825-4eaa-8277-90067c1d9c2c/c-wwwformurldecoder-works-on-winrt-however-not-on-windows-phone-8
                    // var decoder = new WwwFormUrlDecoder(launchUri.Query);

                    var qsParameters = ParseQueryString(launchUri);

                    // Scheme: vie-pt
                    if (0 == String.Compare(ProtocolName, launchUri.Scheme, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Path: Departures
                        if (0 == String.Compare(NavigateToDepartures, launchUri.LocalPath, StringComparison.InvariantCultureIgnoreCase))
                        {
                            string stationIdValue = qsParameters[StationIdParameterName]; // throws Exception if not found
                            int stationId = Int32.Parse(stationIdValue);    // throws too

                            return new Uri("/Views/StationInfo/StationInfoPivotPage.xaml?NavigationStationId=" + stationId,
                                UriKind.Relative);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            // Default: perform normal launch
            return uri;
        }

        // Adapted from http://stackoverflow.com/questions/15888883/is-there-a-simple-way-to-get-query-string-parameters-from-a-uri-in-windows-phone?lq=1
        public static Dictionary<string, string> ParseQueryString(Uri uri)
        {
            var dict = new Dictionary<string, string>();

            if (String.IsNullOrWhiteSpace(uri.Query)) 
                return dict;

            string substring = uri.Query.Substring(1);

            string[] pairs = substring.Split('&');

            foreach (string piece in pairs)
            {
                string[] pair = piece.Split('=');

                if (pair.Length == 2)
                {
                    dict.Add(pair[0], pair[1]);
                }
            }

            return dict;
        }
    }
}
