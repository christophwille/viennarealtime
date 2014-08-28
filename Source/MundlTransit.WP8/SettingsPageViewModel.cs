using System.Diagnostics;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8
{
    public class SettingsPageViewModel : Screen
    {
        private readonly ILocationService _locationService;
        private readonly IConfigurationService _configurationService;

        public SettingsPageViewModel(ILocationService locsvc, IConfigurationService configurationService)
        {
            _locationService = locsvc;
            _configurationService = configurationService;
        }

        // OnInitialize is called only once, we want to do it on every activation of this screen
        protected override void OnActivate()
        {
            base.OnActivate();

            isChecked = _locationService.GetCurrentConsentValue();
            NotifyOfPropertyChange(() => Checked);
        }

        private bool? isChecked;
        public bool? Checked
        {
            get { return isChecked; }
            set
            {
                if (value.HasValue)
                {
                    _locationService.SetConsentValue(value.Value);
                    isChecked = value;

                    NotifyOfPropertyChange(() => Checked);
                }
            }
        }

        private string _progressMessage;
        public string ProgressMessage
        {
            get { return _progressMessage; }
            set
            {
                _progressMessage = value;
                NotifyOfPropertyChange(() => ProgressMessage);
            }
        }

        public async void BuildUserDatabase()
        {
            ApplicationAnalyticsService.Current.LogEvent("Stops/BuildDatabase");
            string dbName = "UserRefDb" + Guid.NewGuid().ToString() + ".db3";

            try
            {
                var ctx = new ReferenceDataContext(dbName);

                // Re-initialize all tables
                await ctx.InitializeDatabaseAsync();

                // Perform import
                var importer = new DefaultImportService(ctx);

                ProgressMessage = AppResources.Settings_Progress_LoadingLines;
                string haltestellen = await importer.DownloadHaltestellenAsync();
                ProgressMessage = AppResources.Settings_Progress_LoadingLines;
                string linien = await importer.DownloadLinienAsync();
                ProgressMessage = AppResources.Settings_Progress_LoadingPlatforms;
                string steige = await importer.DownloadSteigeAsync();

                if (null == haltestellen || null == linien || null == steige)
                {
                    ProgressMessage = AppResources.Settings_Progress_ErrorDownloading;
                    return;
                }

                ProgressMessage = AppResources.Settings_Progress_InsertingInDb;
                int countOfHaltestellen = await importer.ImportHaltestellenAsync(haltestellen);
                int countOfLinien = await importer.ImportLinienAsync(linien);
                int countOfSteige = await importer.ImportSteigeAsync(steige);

                await importer.CreateLookupTableAsync();

                ProgressMessage = String.Format(AppResources.Settings_Progress_ImportSuccessMessage,
                    countOfHaltestellen, countOfLinien, countOfSteige);

                _configurationService.UsingDefaultReferenceDatabase = false;
                _configurationService.CustomReferenceDatabaseName = dbName;
                _configurationService.ReferenceDatabaseBuildDate = DateTime.Now.Date;

                NotifyOfPropertyChange(() => CanRevertToDefault);
                NotifyOfPropertyChange(() => DatabaseBuildDateMessage);
            }
            catch (Exception ex)
            {
                ProgressMessage = AppResources.Settings_Progress_ImportFailed + ex.Message;
                Debug.WriteLine(ex.ToString());
            }
        }

        public bool CanRevertToDefault
        {
            get
            {
                return !_configurationService.UsingDefaultReferenceDatabase;
            }
        }

        public string DatabaseBuildDateMessage
        {
            get
            {
                return _configurationService.ReferenceDatabaseBuildDate.ToShortDateString();
            }
        }

        public void RevertToDefault()
        {
            _configurationService.UsingDefaultReferenceDatabase = true;
            _configurationService.CustomReferenceDatabaseName = String.Empty;
            _configurationService.ReferenceDatabaseBuildDate = ReferenceDataContext.ReferenceDatabaseBuildDate;

            ProgressMessage = "";
            NotifyOfPropertyChange(() => CanRevertToDefault);
            NotifyOfPropertyChange(() => DatabaseBuildDateMessage);
        }
    }
}
