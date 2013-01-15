using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Stations
{
    // "list" "search" "nearby" "line" (use expanderview)
    public class StationsPivotPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly StationsListViewModel listStations;
        private readonly StationsSearchViewModel searchStations;
        private readonly NearbyStationsViewModel nearbyStations;

        public StationsPivotPageViewModel(StationsListViewModel list, StationsSearchViewModel search, NearbyStationsViewModel nearby)
		{
			listStations = list;
            searchStations = search;
            nearbyStations = nearby;

            StationsViewModelOnNavigating = StationsViewModelEnum.NotSet;
		}

        public StationsViewModelEnum StationsViewModelOnNavigating { get; set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(listStations);
            Items.Add(searchStations);
            Items.Add(nearbyStations);

            if (StationsViewModelOnNavigating != StationsViewModelEnum.NotSet)
            {
                var view = Items.First(s => ((StationsViewModelBase)s).StationsViewModel == StationsViewModelOnNavigating);
                ActivateItem(view);
            }
            else
            {
                ActivateItem(listStations);
            }
        }

        public void ShowOnMap()
        {
            nearbyStations.ShowOnMap();
        }

        public void RefreshPosition()
        {
            nearbyStations.RefreshPosition();
        }
    }
}
