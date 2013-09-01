using System.Reflection;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using Windows.ApplicationModel;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8
{
    public class AboutPageViewModel : Screen
    {
        public AboutPageViewModel(IConfigurationService config)
        {
            VersionText = GetAppVersion();

            GitHubUrl = new Uri(config.GitHubUrl);
            PrivacyPolicyUrl = new Uri(config.PrivacyPolicyUrl);
            WienerLinienOgdCatalogPage = new Uri("https://open.wien.at/site/datensatz/?id=add66f20-d033-4eee-b9a0-47019828e698");
        }

        public string VersionText { get; set; }

        public Uri GitHubUrl { get; set; }
        public Uri PrivacyPolicyUrl { get; set; }
        public Uri WienerLinienOgdCatalogPage { get; set; }

        public void Review()
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        private string GetAppVersion()
        {
            var nameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

            var version = nameHelper.Version;
            //var full = nameHelper.FullName;
            //var name = nameHelper.Name;

            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public string AboutParagraphOne { get { return AppResources.About_InfoParagraphOne; }}
        public string AboutParagraphTwo { get { return AppResources.About_InfoParagraphTwo; } }
    }
}
