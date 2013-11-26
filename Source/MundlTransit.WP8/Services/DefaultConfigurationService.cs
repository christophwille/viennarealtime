using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;

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

        #region Isolated Storage Helpers
        private T GetValue<T>(string settingName)
        {
            return (T)IsolatedStorageSettings.ApplicationSettings[settingName];
        }

        public T? TryGetValue<T>(string settingName) where T : struct
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                return GetValue<T>(settingName);
            }

            return (T?)null;
        }

        public string TryGetString(string settingName)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(settingName))
            {
                return GetValue<string>(settingName);
            }

            return String.Empty;
        }

        public void SetValue<T>(string settingName, T value)
        {
            IsolatedStorageSettings.ApplicationSettings[settingName] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        #endregion

        private const string SettingUsingDefaultReferenceDatabase = "UsingDefaultReferenceDatabase";
        private const string SettingCustomReferenceDatabaseName = "CustomReferenceDatabaseName";
        private const string SettingReferenceDatabaseBuildDate = "ReferenceDatabaseBuildDate";

        public bool UsingDefaultReferenceDatabase
        {
            get
            {
                var result = TryGetValue<bool>(SettingUsingDefaultReferenceDatabase);
                return (result == null || result.Value == true);
            }
            set
            {
                SetValue<bool>(SettingUsingDefaultReferenceDatabase, value);
            }
        }

        public string CustomReferenceDatabaseName
        {
            get
            {
                return TryGetString(SettingCustomReferenceDatabaseName);
            }
            set
            {
                SetValue<string>(SettingCustomReferenceDatabaseName, value);
            }
        }

        public DateTime ReferenceDatabaseBuildDate
        {
            get
            {
                var result = TryGetValue<DateTime>(SettingReferenceDatabaseBuildDate);
                if (null != result) 
                    return result.Value;

                return ReferenceDataContext.ReferenceDatabaseBuildDate;
            }
            set
            {
                SetValue<DateTime>(SettingReferenceDatabaseBuildDate, value);
            }
        }
    }
}
