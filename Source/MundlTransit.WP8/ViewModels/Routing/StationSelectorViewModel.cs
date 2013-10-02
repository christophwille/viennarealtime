using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.ViewModels.Stations;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class StationSelectorViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly StationsListViewModel listStations;
        private readonly StationsSearchViewModel searchStations;
        private readonly NearbyStationsViewModel nearbyStations;
        private readonly FavoritesViewModel favorites;

        public StationSelectorViewModel(StationsListViewModel list, StationsSearchViewModel search, NearbyStationsViewModel nearby, FavoritesViewModel fvm)
        {
            favorites = fvm;
            favorites.OnStationPicked = OnStationPicked;

            listStations = list;
            listStations.OnStationPicked = OnStationPicked;

            searchStations = search;
            searchStations.OnStationPicked = OnStationPicked;

            nearbyStations = nearby;
            nearbyStations.OnStationPicked = OnStationPicked;

            DisplayName = AppResources.StationSelector_DisplayName;
        }

        private void OnStationPicked(int stationId, string stationName)
        {
            PickedStation = stationId;
            PickedStationName = stationName;

            TryClose();
        }

        public int? PickedStation { get; protected set; }
        public string PickedStationName { get; protected set; }

        

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(listStations);
            Items.Add(searchStations);
            Items.Add(nearbyStations);
            Items.Add(favorites);

            ActivateItem(listStations);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            favorites.LoadFavoritesAsync();
        }
    }
}
