using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;
using Windows.Devices.Geolocation;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public class NearbyStationsViewModel : StationsViewModelBase
    {
        private readonly ILocationService _locationService;
        private readonly IDataService _dataService;

        public NearbyStationsViewModel(INavigationService navigationService, ILocationService ls, IDataService ds)
            : base(StationsViewModelEnum.Nearby, navigationService)
        {
            _locationService = ls;
            _dataService = ds;

            DisplayName = "nearby";
        }

        private string _infoMessage;
        public string InfoMessage
        {
            get { return _infoMessage; }
            set
            {
                _infoMessage = value;
                NotifyOfPropertyChange(() => InfoMessage);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            PerformNearestSearch();
        }

        public IObservableCollection<Haltestelle> Haltestellen { get; set; }
        private Wgs84Location MyLocation { get; set; }

        // Geolocator works only on the UI thread; won't return at all if not
        protected async void PerformNearestSearch()
        {
            InfoMessage = "Acquiring position...";
            Deployment.Current.Dispatcher.BeginInvoke(ContinueNearestSearchOnUiThread);
        }

        protected async void ContinueNearestSearchOnUiThread()
        {
            Haltestellen = null;
            NotifyOfPropertyChange(() => Haltestellen);

            var posResult = await _locationService.GetCurrentPosition();

            if (posResult.Succeeded)
            {
                var pos = posResult.Position;
                InfoMessage = String.Format("Your position: {0:F2} {1:F2}", pos.Coordinate.Longitude, pos.Coordinate.Latitude);

                MyLocation = new Wgs84Location(pos.Coordinate);

                var haltestellen = await _dataService.GetNearestHaltestellenAsync(MyLocation);

                if (!haltestellen.Any())
                {
                    InfoMessage = String.Format("No stops found near: {0:F2} {1:F2}", 
                        pos.Coordinate.Longitude, pos.Coordinate.Latitude);
                }
                else
                {
                    Haltestellen = new BindableCollection<Haltestelle>(haltestellen.OrderBy(h => h.Distanz));
                    NotifyOfPropertyChange(() => Haltestellen);
                }
            }
            else
            {
                InfoMessage = posResult.ErrorMessage;
            }
        }

        public void ShowOnMap()
        {
            if (null == Haltestellen || !Haltestellen.Any())
                return;

            var ids = Haltestellen.Select(h => h.Id).ToList();
            var idsParam = string.Join(",", ids);

            NavigationService.UriFor<MapNearbyStationsPageViewModel>()
                .WithParam(vm => vm.NavigationNearbyStationIdList, idsParam)
                .WithParam(vm => vm.NavigationLongitudeMe, MyLocation.Longitude)
                .WithParam(vm => vm.NavigationLatitudeMe, MyLocation.Latitude)
                .Navigate();
        }

        public void RefreshPosition()
        {
            PerformNearestSearch();
        }
    }
}
