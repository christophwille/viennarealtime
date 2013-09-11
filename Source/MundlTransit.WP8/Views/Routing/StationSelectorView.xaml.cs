using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MundlTransit.WP8.Views.Routing
{
    public partial class StationSelectorView : UserControl
    {
        public StationSelectorView()
        {
            InitializeComponent();

            Width = Application.Current.Host.Content.ActualWidth;
            Height = Application.Current.Host.Content.ActualHeight;
        }
    }
}
