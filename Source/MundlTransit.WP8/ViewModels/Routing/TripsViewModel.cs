using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class TripsViewModel : Screen
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public TripsViewModel(INavigationService navigationService, IDataService ds)
        {
            _dataService = ds;
            _navigationService = navigationService;
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadTripsAsync();
        }

        protected async Task LoadTripsAsync()
        {
            var request = NewRouteViewModel.CurrentRoutingRequest;

            var tempSchnittstelle = new WienerLinien.Api.Routing.RoutingSchnittstelle();
            var response = await tempSchnittstelle.GetRoutingAsync(request);
        }
    }
}
