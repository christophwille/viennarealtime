using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.ViewModels;
using MundlTransit.WP8.Views;
using ReviewNotifier.WindowsPhoneSilverlight;
using CM = Caliburn.Micro;

namespace MundlTransit.WP8
{
    public partial class MainPage : PhoneApplicationPage, IHandle<PanoramaItemToShowMessage>
    {
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar()
            {
                IsVisible = false
            };

            var refreshAppBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/refresh.png", UriKind.Relative),
                Text = AppResources.Alerts_AppBar_Refresh,
                Message = "RefreshAlerts"
            };

            ApplicationBar.Buttons.Add(refreshAppBarButton);
        }

        public void Handle(PanoramaItemToShowMessage message)
        {
            panoramaMain.SlideToPage(1);
        }

        private void PanoramaMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isTrafficInfoPanoramaItem =
                ((PanoramaItem) (((Panorama) sender).SelectedItem)).Content is TrafficInfoView;
            
            ApplicationBar.IsVisible = isTrafficInfoPanoramaItem;
        }


        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReviewNotification.TriggerAsync(AppResources.ReviewNotifications_Message, AppResources.ReviewNotifications_Title, 5);
        }
    }
}