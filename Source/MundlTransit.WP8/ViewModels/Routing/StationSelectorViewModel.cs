using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class StationSelectorViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public StationSelectorViewModel(FavoritesViewModel fvm)
        {
            Favorites = fvm;

            DisplayName = "select station for routing";
        }

        public FavoritesViewModel Favorites { get; protected set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(Favorites);

            ActivateItem(Favorites);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Favorites.LoadFavoritesAsync();
        }
    }
}
