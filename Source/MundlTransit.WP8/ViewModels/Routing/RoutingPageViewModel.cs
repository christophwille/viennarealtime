using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class RoutingPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IUIService _uiService;

        public RoutingPageViewModel(INavigationService navigationService, IDataService ds, IUIService uisvc, 
            NewRouteViewModel newRouteVm, RouteHistoryViewModel historyVm)
        {
            _navigationService = navigationService;
            _dataService = ds;
            _uiService = uisvc;

            DisplayName = AppResources.RoutingPageView_DisplayName;

            NewRoute = newRouteVm;
            RouteHistory = historyVm;
        }

        public NewRouteViewModel NewRoute { get; protected set; }
        public RouteHistoryViewModel RouteHistory { get; protected set; }

        protected async override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            RouteHistory.LoadRouteHistoryAsync();
        }

        public void ReverseFromTo()
        {
            NewRoute.ReverseFromTo();
        }

        public void SetToNow()
        {
            NewRoute.SetToNow();
        }
    }
}
