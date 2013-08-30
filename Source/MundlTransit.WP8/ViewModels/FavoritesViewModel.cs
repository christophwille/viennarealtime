using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            if (favorites != null && favorites.Any())
            {
                Favorites = new BindableCollection<Favorite>(favorites);
                NotifyOfPropertyChange(() => Favorites);
                SetResultsFound();
            }
            else
            {
                SetNoResultsFound();
            }
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

        public void Remove(object sender, Favorite item)
        {
            // http://stackoverflow.com/questions/14444583/contextmenu-in-datatemplate-binding-issue
            ((sender as MenuItem).Parent as ContextMenu).ClearValue(FrameworkElement.DataContextProperty);

            PerformRemovalAsync(item);
        }

        private async Task PerformRemovalAsync(Favorite item)
        {
            await _dataService.DeleteFavoriteAsync(item);
            await LoadFavoritesAsync();
        }

        public bool ResultsFound { get; set; }
        public bool NoResultsFound { get; set; }

        protected void SetNoResultsFound()
        {
            ResultsFound = false;
            NoResultsFound = true;
            NotifyOfPropertyChange(() => ResultsFound);
            NotifyOfPropertyChange(() => NoResultsFound);
        }

        protected void SetResultsFound()
        {

            NoResultsFound = false;
            ResultsFound = true;
            NotifyOfPropertyChange(() => NoResultsFound);
            NotifyOfPropertyChange(() => ResultsFound);
        }
    }
}
