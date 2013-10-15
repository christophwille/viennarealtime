using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using Newtonsoft.Json;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class TripsViewModel : Screen
    {
        private ProgressIndicator _progressIndicator;

        private readonly IDataService _dataService;
        private readonly IRoutingService _routingService;
        private readonly INavigationService _navigationService;

        public TripsViewModel(INavigationService navigationService, IDataService ds, IRoutingService routingService)
        {
            _dataService = ds;
            _routingService = routingService;
            _navigationService = navigationService;
        }

        public IObservableCollection<RoutingTripModel> Trips { get; set; }

        public string From { get; set; }    // navigation property
        public string To { get; set; }      // navigation property
        public string CurrentRoutingRequest { get; set; }      // navigation property

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadTripsAsync();
        }

        protected RoutingRequest GetCurrentRoutingRequest()
        {
            return JsonConvert.DeserializeObject<RoutingRequest>(CurrentRoutingRequest);
        }

        protected async Task LoadTripsAsync()
        {
            EnableProgressBar();

            var routingRequest = GetCurrentRoutingRequest();
            var response = await _routingService.RetrieveRouteAsync(routingRequest);

            DisableProgressBar();

            if (response.Succeeded)
            {
                Trips = new BindableCollection<RoutingTripModel>(RoutingTripModel.TripsToTripModels(response.Trips));
                NotifyOfPropertyChange(() => Trips);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(AppResources.Routing_Error_RoutesCouldNotBeRetrieved,
                                                            AppResources.ErrorMessage_Title, MessageBoxButton.OK);
            }
        }

        public string FromLabel { get { return AppResources.TripsView_FromLabel; } }
        public string ToLabel { get { return AppResources.TripsView_ToLabel; } }

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
