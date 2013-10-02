using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.ViewModels.Lines;
using MundlTransit.WP8.ViewModels.Routing;
using MundlTransit.WP8.ViewModels.Stations;
using MundlTransit.WP8.Views.Lines;
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
                                        Name = AppResources.MenuItem_Nearby, 
                                        Description=AppResources.MenuItem_Nearby_Description, 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>()
                                            .WithParam(p => p.StationsViewModelOnNavigating, StationsViewModelEnum.Nearby)
                                            .Navigate() 
                                    },
                                    new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_Search, 
                                        Description=AppResources.MenuItem_Search_Description, 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>()
                                            .WithParam(p => p.StationsViewModelOnNavigating, StationsViewModelEnum.Search)
                                            .Navigate() 
                                    },
									new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_StationList, 
                                        Description=AppResources.MenuItem_StationList_Description, 
                                        Navigate = (n) => n.UriFor<StationsPivotPageViewModel>().Navigate() 
                                    },
                                    new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_Routing, 
                                        Description= AppResources.MenuItem_Routing_Description, 
                                        Navigate = (n) => n.UriFor<RoutingPivotPageViewModel>().Navigate() 
                                    },
									new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_StationsByLines, 
                                        Description=AppResources.MenuItem_StationsByLines_Description, 
                                        Navigate = (n) => n.UriFor<LinesPivotPageViewModel>().Navigate() 
                                    },

                                    new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_Settings, 
                                        Description=AppResources.MenuItem_Settings_Description, 
                                        Navigate = (n) => n.UriFor<SettingsPageViewModel>().Navigate() 
                                    },
                                    new MenuItem
                                    { 
                                        Name = AppResources.MenuItem_About, 
                                        Description=AppResources.MenuItem_About_Description, 
                                        Navigate = (n) => n.UriFor<AboutPageViewModel>().Navigate() 
                                    },
								};
        }

        public IObservableCollection<MenuItem> MenuItems { get; private set; }

        public void ExecuteMenuCommand(object sender)
        {
            this.WhenSelectionChanged<MenuItem>(sender, (item) => item.Navigate(navigationService));
        }
    }
}
