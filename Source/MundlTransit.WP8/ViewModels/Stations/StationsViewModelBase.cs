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
    public class StationsViewModelBase : Screen
    {
        protected readonly INavigationService NavigationService;

        public StationsViewModelBase(StationsViewModelEnum lvm, INavigationService navigationService)
        {
            StationsViewModel = lvm;
            this.NavigationService = navigationService;
        }

        public StationsViewModelEnum StationsViewModel { get; set; }

        public void ShowStation(object sender)
        {
            this.WhenSelectionChanged<Haltestelle>(sender, (item) =>
            {
                NavigationService.UriFor<StationInfoPivotPageViewModel>()
                    .WithParam(m => m.NavigationStationId, item.Id)
                    .Navigate();
            });
        }
    }
}
