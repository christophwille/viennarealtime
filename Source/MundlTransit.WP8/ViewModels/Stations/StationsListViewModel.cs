using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;

namespace MundlTransit.WP8.ViewModels.Stations
{
    public class StationsListViewModel : StationsViewModelBase
    {
        private IDataService _dataService;

        public StationsListViewModel(INavigationService navigationService, IDataService ds)
            : base(StationsViewModelEnum.List, navigationService)
        {
            _dataService = ds;
			DisplayName = "list";
		}

        public List<AlphaKeyGroup<Haltestelle>> Haltestellen { get; set; }

	    protected override void OnInitialize()
	    {
            base.OnInitialize();

	        LoadHaltestellenAsync();
	    }

        protected async void LoadHaltestellenAsync()
        {
            var hst = await _dataService.GetHaltestellenAsync();

            // http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj244365%28v=vs.105%29.aspx
            List<AlphaKeyGroup<Haltestelle>> grouped = AlphaKeyGroup<Haltestelle>
                .CreateGroups(hst, Thread.CurrentThread.CurrentUICulture, h => h.Bezeichnung, true);

            Haltestellen = grouped;
            NotifyOfPropertyChange(() => Haltestellen);
        }
	}
}
