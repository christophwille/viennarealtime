using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class RouteHistoryViewModel : Screen
    {
        private readonly IDataService _dataService;

        public RouteHistoryViewModel(IDataService ds)
        {
            _dataService = ds;
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
            // TODO: switch panorama to "New Route" item and set the properties to respective values from history item
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
