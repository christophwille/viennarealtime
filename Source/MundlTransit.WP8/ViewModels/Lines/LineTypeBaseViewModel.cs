using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.LineInfo;
using WienerLinien.Api;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class LineTypeBaseViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public LineTypeBaseViewModel(INavigationService navigationService, IDataService dataSvcService)
        {
            _navigationService = navigationService;
            _dataService = dataSvcService;
        }

        protected void SetSingleLineType(MonitorLineType mlt)
        {
            LineTypes = new List<MonitorLineType>() { mlt };
        }

        public List<MonitorLineType> LineTypes { get; protected set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            LoadLinesAsync();
        }

        public async Task LoadLinesAsync()
        {
            var linien = await _dataService.GetLinienAsync(LineTypes);
            var modelLinien = linien.Select(l => new LinieModel(l)).ToList();

            Lines = new BindableCollection<LinieModel>(modelLinien);
            NotifyOfPropertyChange(() => Lines);
        }

        public BindableCollection<LinieModel> Lines { get; set; }

        public void ShowLine(object sender)
        {
            this.WhenSelectionChanged<LinieModel>(sender, (item) =>
            {
                _navigationService.UriFor<LineInfoPageViewModel>()
                    .WithParam(vm => vm.NavigationLineId, item.Id)
                    .WithParam(vm => vm.NavigationLineName, item.Bezeichnung)
                    .Navigate();
            });
        }
    }
}
