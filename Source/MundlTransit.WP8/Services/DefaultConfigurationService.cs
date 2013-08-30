using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Services
{
    public class DefaultConfigurationService : IConfigurationService
    {
        public string WienerLinienApiKey
        {
            get { return (string)App.Current.Resources["WienerLinienApiKey"]; }
        }

        public string GitHubUrl
        {
            get { return (string)App.Current.Resources["GitHubUrl"]; }
        }

        public string PrivacyPolicyUrl
        {
            get { return (string)App.Current.Resources["PrivacyPolicyUrl"]; }
        }

        public string MapApplicationId
        {
            get { return (string)App.Current.Resources["MapApplicationId"]; }
        }

        public string MapAuthenticationToken
        {
            get { return (string)App.Current.Resources["MapAuthenticationToken"]; }
        }
    }
}
