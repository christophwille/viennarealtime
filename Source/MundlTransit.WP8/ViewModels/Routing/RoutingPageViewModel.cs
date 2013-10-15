using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class RoutingPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly NewRouteViewModel _newRouteViewModel;
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IUIService _uiService;

        public RoutingPageViewModel(INavigationService navigationService, IDataService ds, IUIService uisvc, NewRouteViewModel newRoute)
        {
            _navigationService = navigationService;
            _dataService = ds;
            _uiService = uisvc;
            
            DisplayName = "Routing";
            _newRouteViewModel = newRoute;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(_newRouteViewModel);

            ActivateItem(_newRouteViewModel);
        }

        public void ReverseFromTo()
        {
            _newRouteViewModel.ReverseFromTo();
        }

        public void SetToNow()
        {
            _newRouteViewModel.SetToNow();
        }
    }
}
