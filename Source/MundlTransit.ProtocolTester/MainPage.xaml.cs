﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.System;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MundlTransit.ProtocolTester
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void StationLauncher_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("vie-pt:Departures?StationId=214460793"));  // Kleistgasse
        }

        private void StationLauncherByName_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("vie-pt:Departures?Station=Schwedenplatz"));
        }
    }
}