using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Resources;
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
            DisplayName = AppResources.StationInfo_Departures_Title;
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
        public async Task RefreshDepartureInformationAsync()
        {
            EnableProgressBar();

            var monitorInfo = await _echtzeitdatenService.RetrieveMonitorInformationAsync(Haltestelle);

            DisableProgressBar();

            if (monitorInfo.Succeeded && monitorInfo.Lines.Any())
            {
                Departures = monitorInfo.Lines;
            }
            else
            {
                string errMessage = AppResources.MonitorError_FallbackOnErrorToErrorMessageConversion;

                if (!monitorInfo.Succeeded)
                {
                    errMessage = new DefaultMonitorErrorToErrorMessageService().GetMessage(monitorInfo.ErrorCode);
                }

                MessageBoxResult result =
                    MessageBox.Show(errMessage,
                    AppResources.ErrorMessage_Title,
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

            _progressIndicator.Text = AppResources.ProgressMessage_LoadingDepartures;
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
