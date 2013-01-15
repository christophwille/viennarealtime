using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public enum StationsViewModelEnum
    {
        NotSet = 0,
        List,
        Search,
        Nearby
    }

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
            var ll = sender as LongListSelector;
            var item = ll.SelectedItem as Haltestelle;
            if (item == null) return;

            ll.SelectedItem = null;

            NavigationService.UriFor<StationInfoPivotPageViewModel>()
                .WithParam(m => m.NavigationStationId, item.Id)
                .Navigate();
        }
    }
}
