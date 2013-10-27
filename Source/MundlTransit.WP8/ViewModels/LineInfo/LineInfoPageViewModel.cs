using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.ViewModels.LineInfo
{
    public class LineInfoPageViewModel : Screen
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public LineInfoPageViewModel(INavigationService navigationService, IDataService ds)
        {
            _dataService = ds;
            _navigationService = navigationService;
        }

        public int NavigationLineId { get; set; }
        public string NavigationLineName { get; set; }
        public string LineDirection { get; set; }

        public BindableCollection<LinienHaltestelleView> Stations { get; set; }

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadLinienStationsAsync();
        }

        private List<LinienHaltestelleView> _haltestellen;
        public string Richtung { get; set; }

        protected async Task LoadLinienStationsAsync()
        {
            _haltestellen = await _dataService.GetHaltestellenForLinieAsync(NavigationLineId);

            Richtung = OgdLinie.Retour;
            ChangeDirection();
        }

        public void ChangeDirection()
        {
            Richtung = (Richtung == OgdLinie.Retour) ? OgdLinie.Hin : OgdLinie.Retour;

            var stations = _haltestellen.Where(h => h.Richtung == Richtung).OrderBy(h => h.Reihenfolge).ToList();

            Stations = new BindableCollection<LinienHaltestelleView>(stations);
            NotifyOfPropertyChange(() => Stations);

            // There could be no stations
            LineDirection = stations.Any() ? stations.Last().Bezeichnung : "";
            
            NotifyOfPropertyChange(() => LineDirection);
        }

        public void ShowStation(object sender)
        {
            this.WhenSelectionChanged<Haltestelle>(sender, (item) =>
            {
                _navigationService.UriFor<StationInfoPivotPageViewModel>()
                    .WithParam(m => m.NavigationStationId, item.Id)
                    .Navigate();
            });
        }
    }
}
