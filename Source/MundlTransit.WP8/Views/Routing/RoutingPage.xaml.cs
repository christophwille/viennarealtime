using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Model;
using CM = Caliburn.Micro;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Resources;

namespace MundlTransit.WP8.Views.Routing
{
    public partial class RoutingPage : PhoneApplicationPage, IHandle<ShowNewRouteViewMessage>
    {
        public RoutingPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        public void Handle(ShowNewRouteViewMessage message)
        {
            RoutingPanorama.SlideToPage(0);
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