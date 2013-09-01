using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.ViewModels.Stations;
using CM = Caliburn.Micro;

namespace MundlTransit.WP8.Views.Stations
{
    public partial class StationsPivotPage : PhoneApplicationPage
    {
        public StationsPivotPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            var mapAppBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/map.centerme.png", UriKind.Relative),
                Text = "map",
                Message = "ShowOnMap"
            };

            ApplicationBar.Buttons.Add(mapAppBarButton);

            var refreshAppBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/refresh.png", UriKind.Relative),
                Text = "refresh",
                Message = "RefreshPosition"
            };

            ApplicationBar.Buttons.Add(refreshAppBarButton);
        }

        //
        // http://stackoverflow.com/questions/6007721/is-it-possible-to-show-application-bar-for-one-pivot-item-only
        // http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh394036%28v=vs.105%29.aspx
        //
        private void Items_OnLoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            bool isNearbyPivot = ((Pivot)sender).SelectedItem is NearbyStationsViewModel;
            ApplicationBar.IsVisible = isNearbyPivot;
        }
    }
}