using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Shell;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.Stations;

namespace MundlTransit.WP8.Views.Stations
{
    public partial class MapNearbyStationsPage : PhoneApplicationPage, IHandle<ZoomMapToPinsMessage>
    {
        public MapNearbyStationsPage()
        {
            InitializeComponent();

            MapExtensionsSetup(this.Map);

            IoC.Get<IEventAggregator>().Subscribe(this);

            Loaded += OnLoaded;
        }

        private bool _isLoaded = false;
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _isLoaded = true;
            Handle(null);
        }

        private bool _isZoomed = false;
        public void Handle(ZoomMapToPinsMessage message)
        {
            if (!_isLoaded || _isZoomed) return;

            _isZoomed = true;
            var vm = (MapNearbyStationsPageViewModel)DataContext;

            this.UserLocationMarker.GeoCoordinate = new GeoCoordinate(vm.NavigationLatitudeMe, vm.NavigationLongitudeMe);
            this.UserLocationMarker.Visibility = Visibility.Visible;

            this.NearbyStationsMapItemsControl.ItemsSource = vm.NearbyStations;

            var locationRectangle = LocationRectangle.CreateBoundingRectangle(from h in vm.NearbyStations select h.GeoCoordinate);
            Map.SetView(locationRectangle, new Thickness(20, 20, 20, 20));
        }  

        /// <summary>
        /// Setup the map extensions objects.
        /// All named objects inside the map extensions will have its references properly set
        /// </summary>
        /// <param name="map">The map that uses the map extensions</param>
        private void MapExtensionsSetup(Map map)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(map);
            var runtimeFields = this.GetType().GetRuntimeFields();

            foreach (DependencyObject i in children)
            {
                var info = i.GetType().GetProperty("Name");

                if (info != null)
                {
                    string name = (string)info.GetValue(i);

                    if (name != null)
                    {
                        foreach (FieldInfo j in runtimeFields)
                        {
                            if (j.Name == name)
                            {
                                j.SetValue(this, i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            var config = IoC.Get<IConfigurationService>();

            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = config.MapApplicationId;
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = config.MapAuthenticationToken;
#endif
        }
    }
}