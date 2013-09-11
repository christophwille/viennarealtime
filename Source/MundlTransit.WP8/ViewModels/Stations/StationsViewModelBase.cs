using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public class StationsViewModelBase : Screen, IStationPicker
    {
        protected readonly INavigationService NavigationService;

        public StationsViewModelBase(StationsViewModelEnum lvm, INavigationService navigationService)
        {
            StationsViewModel = lvm;
            this.NavigationService = navigationService;

            OnStationPicked = (stationId) => this.NavigationService.UriFor<StationInfoPivotPageViewModel>()
                        .WithParam(vm => vm.NavigationStationId, stationId)
                        .Navigate();
        }

        public StationsViewModelEnum StationsViewModel { get; set; }

        public Action<int> OnStationPicked { get; set; }

        public void ShowStation(object sender)
        {
            this.WhenSelectionChanged<Haltestelle>(sender, (item) => OnStationPicked(item.Id));
        }
    }
}
