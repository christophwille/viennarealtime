using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CM = Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Resources;

namespace MundlTransit.WP8.Views.Routing
{
    public partial class RoutingPage : PhoneApplicationPage
    {
        public RoutingPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            var reverseAppBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/sort.png", UriKind.Relative),
                Text = AppResources.Routing_AppBar_Reverse,
                Message = "ReverseFromTo"
            };

            ApplicationBar.Buttons.Add(reverseAppBarButton);

            var nowAppBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/time.png", UriKind.Relative),
                Text = AppResources.Routing_AppBar_Now,
                Message = "SetToNow"
            };

            ApplicationBar.Buttons.Add(nowAppBarButton);
        }
    }
}