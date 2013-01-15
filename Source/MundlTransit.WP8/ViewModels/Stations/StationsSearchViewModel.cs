using System.Windows.Threading;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public class StationsSearchViewModel : StationsViewModelBase
    {
        private const int MinimumSearchStringLength = 3;
        private const int TimerDelayForAutoSearchInMilliseconds = 250;
 
        private DispatcherTimer _delayTimer;
        private IDataService _dataService;

        public StationsSearchViewModel(INavigationService navigationService, IDataService ds)
            : base(StationsViewModelEnum.Search, navigationService)
        {
            _dataService = ds;
            DisplayName = "search";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _delayTimer = new DispatcherTimer();
            _delayTimer.Tick += OnInputDelayElapsed;

            _delayTimer.Interval = TimeSpan.FromMilliseconds(TimerDelayForAutoSearchInMilliseconds);
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);

                if (CanStartSearch(_searchText))
                {
                    _delayTimer.Start();
                }
            }
        }

        private bool CanStartSearch(string searchString)
        {
            return (searchString.Length >= MinimumSearchStringLength);
        }

        void OnInputDelayElapsed(object sender, EventArgs e)
        {
            _delayTimer.Stop();

            SearchForMatchesAsync(_searchText);
        }

        public IObservableCollection<Haltestelle> Haltestellen { get; set; }

        public async void SearchForMatchesAsync(string searchString)
        {
            if (!CanStartSearch(searchString))
                return;

            Haltestellen = null;
            NotifyOfPropertyChange(() => Haltestellen);

            var result = await _dataService.GetHaltestellenContainingAsync(searchString);

            Haltestellen = new BindableCollection<Haltestelle>(result);
            NotifyOfPropertyChange(() => Haltestellen);
        }
    }
}
