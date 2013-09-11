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
    public partial class RoutingPivotPage : PhoneApplicationPage
    {
        public RoutingPivotPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            var appBarButton = new CM.AppBarButton()
            {
                IconUri = new Uri("/Assets/sort.png", UriKind.Relative),
                Text = "reverse",
                Message = "ReverseFromTo"
            };

            ApplicationBar.Buttons.Add(appBarButton);
        }
    }
}