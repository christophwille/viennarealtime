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

namespace MundlTransit.WP8
{
    public partial class MainPage : PhoneApplicationPage, IHandle<PanoramaItemToShowMessage>
    {
        public MainPage()
        {
            InitializeComponent();

            IoC.Get<IEventAggregator>().Subscribe(this);
        }

        public void Handle(PanoramaItemToShowMessage message)
        {
            panoramaMain.SlideToPage(1);
        }  
    }
}