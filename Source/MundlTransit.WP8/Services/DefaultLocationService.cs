using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MundlTransit.WP8.Model;
using Windows.Devices.Geolocation;

namespace MundlTransit.WP8.Services
{
    public class DefaultLocationService : ILocationService
    {
        public async Task<LocationResult> GetCurrentPosition()
        {
            try
            {
                if (!CheckConsentAskIfNotSet())
                {
                    return new LocationResult("No consent for location");
                }

                var geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 50;

                Geoposition pos = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(1),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                return new LocationResult(pos);
            }
            catch (UnauthorizedAccessException)
            {
                // We were not allowed the proper right for geolocation
                return new LocationResult("Location turned off / denied");
            }
            catch (Exception)
            {
            }

            return new LocationResult("Location could not be obtained");
        }

        private const string LocationConsentSetting = "LocationConsent";

        public bool CheckConsentAskIfNotSet()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(LocationConsentSetting))
            {
                return GetConsentValue();
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings[LocationConsentSetting] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings[LocationConsentSetting] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();

                return GetConsentValue();
            }
        }

        private bool GetConsentValue()
        {
            return (bool)IsolatedStorageSettings.ApplicationSettings[LocationConsentSetting];
        }

        public bool? GetCurrentConsentValue()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(LocationConsentSetting))
            {
                return GetConsentValue();
            }

            return null;
        }

        public void SetConsentValue(bool value)
        {
            IsolatedStorageSettings.ApplicationSettings[LocationConsentSetting] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
