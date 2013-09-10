using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public class MapNearbyStationsPageViewModel : Screen
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        public MapNearbyStationsPageViewModel(IDataService ds, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _dataService = ds;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
        }

        public double NavigationLongitudeMe { get; set; }
        public double NavigationLatitudeMe { get; set; }
        public string NavigationNearbyStationIdList { get; set; }

        public IObservableCollection<MapHaltestelleModel> NearbyStations { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();
            PopulateMapAsync();
        }

        private async Task PopulateMapAsync()
        {
            var idsAsString = NavigationNearbyStationIdList.Split(new char[] { ',' });
            var ids = idsAsString.Select(Int32.Parse).ToList();

            var hst = await _dataService.GetHaltestellenAsync(ids);
            var mapPins = new List<MapHaltestelleModel>();

            foreach (var h in hst)
            {
                try
                {
                    var mapHst = new MapHaltestelleModel()
                                {
                                    Id = h.Id,
                                    Bezeichnung = h.Bezeichnung,
                                    GeoCoordinate = new GeoCoordinate(h.Latitude, h.Longitude)
                                };

                    mapPins.Add(mapHst);
                }
                catch (Exception)
                {
                    // ArgumentOutOfRangeException from new GeoCoordinate(..., ...): we then do not show this station
                }
            }

            NearbyStations = new BindableCollection<MapHaltestelleModel>(mapPins);
            NotifyOfPropertyChange(() => NearbyStations);

            _eventAggregator.Publish(new ZoomMapToPinsMessage());
        }

        public void Show(MapHaltestelleModel h)
        {
            _navigationService.UriFor<StationInfoPivotPageViewModel>()
                    .WithParam(m => m.NavigationStationId, h.Id)
                    .Navigate();
        }
    }
}
