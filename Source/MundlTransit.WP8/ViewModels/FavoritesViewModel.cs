using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels
{
    public class FavoritesViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IDataService _dataService;
        private readonly IUIService _uiService;

        public FavoritesViewModel(INavigationService navigationService, IDataService ds, IUIService uisvc)
        {
            _dataService = ds;
            this.navigationService = navigationService;
            _uiService = uisvc;
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
            this.WhenSelectionChanged<Favorite>(sender, (item) =>
            {
                navigationService.UriFor<StationInfoPivotPageViewModel>()
                    .WithParam(vm => vm.NavigationStationId, item.HaltestellenId)
                    .Navigate();
            });
        }

        public void PinToStart(object sender, Favorite item)
        {
            FixUpBindingIssue(sender);

            string navigationUrl = String.Format(
                "/Views/StationInfo/StationInfoPivotPage.xaml?NavigationStationId={0}", item.HaltestellenId);

            if (null != FindTile(navigationUrl))
            {
                _uiService.ShowTextToast(AppResources.Favorites_Favorite_AlreadyPinned, item.Bezeichnung);
                return;
            }

            ShellTileData tile = new StandardTileData
            {
                Title = item.Bezeichnung,
                BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMediumSecondaryTile.png", UriKind.Relative),
            };

            ShellTile.Create(new Uri(navigationUrl, UriKind.Relative), tile);
        }

        private ShellTile FindTile(string partOfUri)
        {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(
                tile => tile.NavigationUri.ToString().Contains(partOfUri));

            return shellTile;
        }

        public void Remove(object sender, Favorite item)
        {
            FixUpBindingIssue(sender);
            PerformRemovalAsync(item);
        }

        private void FixUpBindingIssue(object sender)
        {
            // http://stackoverflow.com/questions/14444583/contextmenu-in-datatemplate-binding-issue
            ((sender as MenuItem).Parent as ContextMenu).ClearValue(FrameworkElement.DataContextProperty);
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
