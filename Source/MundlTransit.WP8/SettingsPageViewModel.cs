using System.Diagnostics;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
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
            string dbName = "UserRefDb" + Guid.NewGuid().ToString() + ".db3";

            try
            {
                var ctx = new ReferenceDataContext(dbName);

                // Re-initialize all tables
                await ctx.InitializeDatabaseAsync();

                // Perform import
                var importer = new DefaultImportService(ctx);

                ProgressMessage = "Loading Haltestellen... (1 of 3)";
                string haltestellen = await importer.DownloadHaltestellenAsync();
                ProgressMessage = "Loading Linien... (2 of 3)";
                string linien = await importer.DownloadLinienAsync();
                ProgressMessage = "Loading Steige... (3 of 3)";
                string steige = await importer.DownloadSteigeAsync();

                if (null == haltestellen || null == linien || null == steige)
                {
                    ProgressMessage = "Error downloading reference CSV files from data.wien.gv.at";
                    return;
                }

                ProgressMessage = "Inserting data into database...";
                int countOfHaltestellen = await importer.ImportHaltestellenAsync(haltestellen);
                int countOfLinien = await importer.ImportLinienAsync(linien);
                int countOfSteige = await importer.ImportSteigeAsync(steige);

                await importer.CreateLookupTableAsync();

                ProgressMessage = String.Format("Import completed successfully. {0} Haltestellen, {1} Linien, {2} Steige",
                    countOfHaltestellen, countOfLinien, countOfSteige);

                _configurationService.UsingDefaultReferenceDatabase = false;
                _configurationService.CustomReferenceDatabaseName = dbName;
            }
            catch (Exception ex)
            {
                ProgressMessage = "Import failed. Error message: " + ex.Message;
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
