using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;
using WienerLinien.Api;

namespace MundlTransit.WP8.ViewModels.StationInfo
{
    public class DepartureViewModel : StationInfoViewModelBase
    {
        private ProgressIndicator _progressIndicator;
        private readonly IEchtzeitdatenService _echtzeitdatenService;

        public DepartureViewModel(IEchtzeitdatenService wlds)
        {
            _echtzeitdatenService = wlds;
            DisplayName = "departures";
        }

        public Haltestelle Haltestelle { get; set; }

        private List<MonitorLine> _departures;
        public List<MonitorLine> Departures
        {
            get { return _departures; }
            set
            {
                _departures = value;
                NotifyOfPropertyChange(() => Departures);
            }
        }
        public async void RefreshDepartureInformationAsync()
        {
            EnableProgressBar();

            var lines = await _echtzeitdatenService.RetrieveMonitorInformation(Haltestelle);

            DisableProgressBar();

            if (lines != null && lines.Any())
            {
                Departures = lines;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("Realtime data could not be downloaded, please try again later.",
                    "Server Error",
                    MessageBoxButton.OK);
            }
        }

        private void EnableProgressBar()
        {
            if (null == _progressIndicator)
            {
                _progressIndicator = new ProgressIndicator();
                _progressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator = _progressIndicator;
            }

            _progressIndicator.Text = "Loading departures...";
            _progressIndicator.IsIndeterminate = true;
        }

        private void DisableProgressBar()
        {
            // _progressIndicator.IsVisible = false;

            _progressIndicator.Text = "";
            _progressIndicator.IsIndeterminate = false;
        }
    }
}
