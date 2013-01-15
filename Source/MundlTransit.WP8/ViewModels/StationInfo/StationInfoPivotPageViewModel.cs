using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.StationInfo
{
    public class StationInfoPivotPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly DepartureViewModel departureViewModel;
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public StationInfoPivotPageViewModel(INavigationService navigationService,IDataService ds, DepartureViewModel dep)
        {
            _navigationService = navigationService;
            _dataService = ds;
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

        protected override void OnActivate()
        {
            base.OnActivate();
            LoadStationAsync();
        }

        protected async void LoadStationAsync()
        {
            _haltestelle = await _dataService.GetHaltestelleAsync(NavigationStationId);
            departureViewModel.Haltestelle = _haltestelle;

#if !DEBUG
            // In Release (Ship) Mode, auto-get the departure information
            departureViewModel.RefreshDepartureInformationAsync();
#endif

            StationName = _haltestelle.Bezeichnung;
            NotifyOfPropertyChange(() => StationName);
        }

        public void AddToFavorites()
        {
            if (null == _haltestelle) return;

            var fav = new Favorite()
                          {
                              Bezeichnung = _haltestelle.Bezeichnung,
                              HaltestellenId = _haltestelle.Id
                          };

            _dataService.InsertFavoriteIfNotExistsAsync(fav);
        }

        public void RefreshDepartureInformation()
        {
            departureViewModel.RefreshDepartureInformationAsync();
        }

        public void GoToStart()
        {
            _navigationService.UriFor<MainPageViewModel>().Navigate();
        }
    }
}
