using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class RouteHistoryViewModel : Screen
    {
        private readonly IDataService _dataService;
        private readonly IEventAggregator eventAggregator;

        public RouteHistoryViewModel(IDataService ds, IEventAggregator events)
        {
            _dataService = ds;
            eventAggregator = events;
        }

        public async Task LoadRouteHistoryAsync()
        {
            var items = await _dataService.GetRouteHistoryItemsAsync();

            if (items != null && items.Any())
            {
                RouteHistoryItems = new BindableCollection<RouteHistoryItem>(items);
                NotifyOfPropertyChange(() => RouteHistoryItems);
                SetResultsFound();
            }
            else
            {
                SetNoResultsFound();
            }
        }

        public void ShowRouteHistoryItem(object sender)
        {
            this.WhenSelectionChanged<RouteHistoryItem>(sender, (item) =>
            {
                var msg = new ShowNewRouteViewMessage(item);

                // TODO: weird animation effect in RoutingPage/Panorama
                eventAggregator.Publish(msg);
            });
        }

        public IObservableCollection<RouteHistoryItem> RouteHistoryItems { get; private set; }
        public bool ResultsFound { get; set; }
        public bool NoResultsFound { get; set; }

        protected void SetNoResultsFound()
        {
            ResultsFound = false;
            NoResultsFound = true;
            NotifyOfPropertyChange(() => ResultsFound);
            NotifyOfPropertyChange(() => NoResultsFound);
        }

        protected void SetResultsFound()
        {

            NoResultsFound = false;
            ResultsFound = true;
            NotifyOfPropertyChange(() => NoResultsFound);
            NotifyOfPropertyChange(() => ResultsFound);
        }
    }
}
