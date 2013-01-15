using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.ViewModels.Stations;
using MenuItem = MundlTransit.WP8.Model.MenuItem;

namespace MundlTransit.WP8.ViewModels
{
	public class MenuViewModel : Screen
	{
		private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;

        public MenuViewModel(INavigationService navigationService, IEventAggregator events)
		{
			this.navigationService = navigationService;
            eventAggregator = events;

            MenuItems = new BindableCollection<MenuItem>()
								{
                                    //new MenuItem
                                    //{ 
                                    //    Name = "favorites", 
                                    //    Description="your favorite stations list", 
                                    //    Navigate = (n) => eventAggregator.Publish(new PanoramaItemToShow())
                                    //},
                                    new MenuItem
                                    { 
                                        Name = "nearby", 
                                        Description="stations nearby your current location", 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>()
                                            .WithParam(p => p.StationsViewModelOnNavigating, StationsViewModelEnum.Nearby)
                                            .Navigate() 
                                    },
                                    new MenuItem
                                    { 
                                        Name = "search", 
                                        Description="stations by name (fulltext)", 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>()
                                            .WithParam(p => p.StationsViewModelOnNavigating, StationsViewModelEnum.Search)
                                            .Navigate() 
                                    },
									new MenuItem
                                    { 
                                        Name = "station list", 
                                        Description="complete list of stations", 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>().Navigate() 
                                    },

                                    new MenuItem
                                    { 
                                        Name = "settings", 
                                        Description="turn on/off geolocation for nearby search", 
                                        Navigate = (n) => n.UriFor<SettingsPageViewModel>().Navigate() 
                                    },
                                    new MenuItem
                                    { 
                                        Name = "about", 
                                        Description="privacy policy, support", 
                                        Navigate = (n) => n.UriFor<AboutPageViewModel>().Navigate() 
                                    },
								};
		}

        public IObservableCollection<MenuItem> MenuItems { get; private set; }

        public void ExecuteMenuCommand(object sender)
        {
            var ll = sender as LongListSelector;
            var item = ll.SelectedItem as MenuItem;
            if (item == null) return;

            ll.SelectedItem = null;

            item.Navigate(navigationService);
        }
	}
}
