using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.ApplicationInsights.Telemetry.WindowsStore;
using Microsoft.Phone.Tasks;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.StationInfo
{
    public class StationInfoPivotPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly DepartureViewModel departureViewModel;
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IUIService _uiService;

        public StationInfoPivotPageViewModel(INavigationService navigationService, IDataService ds, IUIService uisvc, DepartureViewModel dep)
        {
            _navigationService = navigationService;
            _dataService = ds;
            _uiService = uisvc;
            departureViewModel = dep;
        }

        public string StationName { get; set; }
        public int NavigationStationId { get; set; }

        private Haltestelle _haltestelle;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(departureViewModel);

            ActivateItem(departureViewModel);
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadStationAsync();
        }

        protected async Task LoadStationAsync()
        {
            ClientAnalyticsChannel.Default.LogEvent("Stops/LoadDepartures");

            _haltestelle = await _dataService.GetHaltestelleAsync(NavigationStationId);
            departureViewModel.Haltestelle = _haltestelle;

#if !DEBUG
            // In Release (Ship) Mode, auto-get the departure information
            departureViewModel.RefreshDepartureInformationAsync();
#endif

            StationName = _haltestelle.Bezeichnung;
            NotifyOfPropertyChange(() => StationName);
        }

        public async void AddToFavorites()
        {
            if (null == _haltestelle) return;

            var fav = new Favorite()
                          {
                              Bezeichnung = _haltestelle.Bezeichnung,
                              HaltestellenId = _haltestelle.Id
                          };

            _dataService.InsertFavoriteIfNotExistsAsync(fav);

            _uiService.ShowTextToast(AppResources.Message_FavoriteAdded, _haltestelle.Bezeichnung);
        }

        public void Share()
        {
            if (null == _haltestelle) return;

            string title = String.Format("{0} {1}", AppResources.ShareDeparture_Departures, _haltestelle.Bezeichnung);
            string link = String.Format("vie-pt:Departures?StationId={0}", _haltestelle.Id);

            var task = new ShareLinkTask()
            {
                Title = title,
                LinkUri = new Uri(link, UriKind.Absolute),
                Message = AppResources.ShareDeparture_Message
            };

            task.Show();
        }

        public async void WalkTo()
        {
            if (null == _haltestelle) return;

            Uri uri = new Uri("ms-walk-to:?destination.latitude=" + _haltestelle.Latitude +
                "&destination.longitude=" + _haltestelle.Longitude + "&destination.name=" + _haltestelle.Bezeichnung);

            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        public async void RefreshDepartureInformation()
        {
            departureViewModel.RefreshDepartureInformationAsync();
        }

        public void GoToStart()
        {
            _navigationService.UriFor<MainPageViewModel>().Navigate();
        }
    }
}
