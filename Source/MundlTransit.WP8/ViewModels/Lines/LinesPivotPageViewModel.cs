using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Services;
using WienerLinien.Api;
using WienerLinien.Api.Ogd;

namespace MundlTransit.WP8.ViewModels.Lines
{
    // "list" "search" "nearby" "line" (use expanderview)
    public class LinesPivotPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MetroViewModel mvm;
        private TramViewModel tvm;
        private BusViewModel bvm;
        private NightBusViewModel nbvm;

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public LinesPivotPageViewModel(MetroViewModel mvm, TramViewModel tvm, BusViewModel bvm, NightBusViewModel nbvm, INavigationService navigationService, IDataService ds)
        {
            this.mvm = mvm;
            this.tvm = tvm;
            this.bvm = bvm;
            this.nbvm = nbvm;

            _navigationService = navigationService;
            _dataService = ds;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(mvm);
            Items.Add(tvm);
            Items.Add(bvm);
            Items.Add(nbvm);
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            LoadLinienAsync();
        }

        protected async Task LoadLinienAsync()
        {
            var linien = await _dataService.GetLinienAsync();

            // TODO: Rewrite, inefficient as is
            var metros = linien.Where(l => MonitorLineTypeMapper.TypeStringToType(l.Verkehrsmittel) == MonitorLineType.Metro);
            var trams = linien.Where(l => MonitorLineTypeMapper.TypeStringToType(l.Verkehrsmittel) == MonitorLineType.Tram);
            var buses = linien
                .Where(l => MonitorLineTypeMapper.TypeStringToType(l.Verkehrsmittel) == MonitorLineType.Bus || MonitorLineTypeMapper.TypeStringToType(l.Verkehrsmittel) == MonitorLineType.BusB);
            var nightbuses = linien.Where(l => MonitorLineTypeMapper.TypeStringToType(l.Verkehrsmittel) == MonitorLineType.NightBus);

            mvm.SetLines(metros);
            tvm.SetLines(trams);
            bvm.SetLines(buses);
            nbvm.SetLines(nightbuses);
        }
    }
}
