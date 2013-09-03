using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using WienerLinien.Api;

namespace MundlTransit.WP8.ViewModels
{
    public class TrafficInfoViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IEchtzeitdatenService _echtzeitService;

        private string _message;
        private bool _showResults;
        private bool _showMessage;

        public TrafficInfoViewModel(INavigationService navigationService, IEchtzeitdatenService echtzeitService)
        {
            _navigationService = navigationService;
            _echtzeitService = echtzeitService;
        }

        public async Task LoadTrafficInfoAsync()
        {
            SetToMessage(AppResources.Alerts_AlertsLoading);

            var trafficInfo = await _echtzeitService.RetrieveTrafficInfoListAsync();

            if (trafficInfo.Succeeded)
            {
                if (trafficInfo.Items.Any())
                {
                    TrafficInformation = new BindableCollection<TrafficInformationItem>(trafficInfo.Items);
                    NotifyOfPropertyChange(() => TrafficInformation);
                    SetToResults();
                }
                else
                {
                    SetToMessage(AppResources.Alerts_NoAlerts);
                }
            }
            else
            {
                SetToMessage(AppResources.Alerts_LoadingFailed);
            }
        }

        public void ClearTrafficInformation()
        {
            TrafficInformation = null;
            NotifyOfPropertyChange(() => TrafficInformation);
        }

        public IObservableCollection<TrafficInformationItem> TrafficInformation { get; private set; }

        public string Message
        {
            get { return _message; }
            set { _message = value; NotifyOfPropertyChange(() => Message); }
        }

        public bool ShowResults
        {
            get { return _showResults; }
            set { _showResults = value; NotifyOfPropertyChange(() => ShowResults); }
        }

        public bool ShowMessage
        {
            get { return _showMessage; }
            set { _showMessage = value; NotifyOfPropertyChange(() => ShowMessage); }
        }

        protected void SetToMessage(string message)
        {
            Message = message;
            ShowResults = false;
            ShowMessage = true;
        }

        protected void SetToResults()
        {
            ShowMessage = false;
            ShowResults = true;
        }

        public void ShowTrafficInfo(object sender)
        {
            this.WhenSelectionChanged<TrafficInformationItem>(sender, (item) =>
            {
                string startTime = item.Start.HasValue ? item.Start.Value.ToShortTimeString() : "";
                string message = String.Format("{0}\r\n\r\n{1}: {2}", item.Description, AppResources.Alerts_AlertSince, startTime);

                MessageBox.Show(message, item.Title, MessageBoxButton.OK);
            });
        }
    }
}
