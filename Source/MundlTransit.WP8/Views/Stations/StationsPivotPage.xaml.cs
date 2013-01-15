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

namespace MundlTransit.WP8.Views.Stations
{
    public partial class StationsPivotPage : PhoneApplicationPage
    {
        public StationsPivotPage()
        {
            InitializeComponent();
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