using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class TripsViewModel : Screen
    {
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

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadTripsAsync();
        }

        protected async Task LoadTripsAsync()
        {
            var request = NewRouteViewModel.CurrentRoutingRequest;

            var response = await _routingService.RetrieveRouteAsync(request);

            if (response.Succeeded)
            {
                Trips = new BindableCollection<RoutingTripModel>(response.Trips.Select(t => new RoutingTripModel(t)));
                NotifyOfPropertyChange(() => Trips);
            }
            else
            {
                // TODO Error handling analogous to retrieving departure information
            }
        }
    }
}
