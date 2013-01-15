using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels
{
    public class FavoritesViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IDataService _dataService;

        public FavoritesViewModel(INavigationService navigationService, IDataService ds)
        {
            _dataService = ds;
            this.navigationService = navigationService;
        }

        public async Task LoadFavoritesAsync()
        {
            var favorites = await _dataService.GetFavoritesAsync();

            Favorites = new BindableCollection<Favorite>(favorites);
            NotifyOfPropertyChange(() => Favorites);
        }

        public IObservableCollection<Favorite> Favorites { get; private set; }

        public void ShowFavorite(object sender)
        {
            var ll = sender as LongListSelector;
            var item = ll.SelectedItem as Favorite;
            if (item == null) return;

            ll.SelectedItem = null;

            navigationService.UriFor<StationInfoPivotPageViewModel>()
                .WithParam(vm => vm.NavigationStationId, item.HaltestellenId)
                .Navigate();
        }

        public void Remove(Favorite item)
        {
            PerformRemovalAsync(item);
        }

        private async void PerformRemovalAsync(Favorite item)
        {
            await _dataService.DeleteFavoriteAsync(item);
            await LoadFavoritesAsync();
        }
    }
}
