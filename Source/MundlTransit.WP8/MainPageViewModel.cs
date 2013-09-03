using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.ViewModels;

namespace MundlTransit.WP8
{
    public class MainPageViewModel : Screen
    {
        public MainPageViewModel(MenuViewModel mvm, TrafficInfoViewModel tivm, FavoritesViewModel fvm)
        {
            Menu = mvm;
            TrafficInfo = tivm;
            Favorites = fvm;
        }

        public MenuViewModel Menu { get; protected set; }
        public FavoritesViewModel Favorites { get; protected set; }
        public TrafficInfoViewModel TrafficInfo { get; protected set; }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            Favorites.LoadFavoritesAsync();
            TrafficInfo.LoadTrafficInfoAsync();
        }

        public void RefreshAlerts()
        {
            TrafficInfo.ClearTrafficInformation();
            TrafficInfo.LoadTrafficInfoAsync();
        }
    }
}
