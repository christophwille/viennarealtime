using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Model;
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

        public LinesPivotPageViewModel(MetroViewModel mvm, TramViewModel tvm, BusViewModel bvm, NightBusViewModel nbvm, IDataService ds)
        {
            this.mvm = mvm;
            this.tvm = tvm;
            this.bvm = bvm;
            this.nbvm = nbvm;

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
            var modelLinien = linien.Select(l => new LinieModel(l)).ToList();

            var metros = modelLinien.Where(l => l.LineType == MonitorLineType.Metro).OrderBy(l => l.Reihenfolge);
            var trams = modelLinien.Where(l => l.LineType == MonitorLineType.Tram).OrderBy(l => l.Reihenfolge);
            var buses = modelLinien.Where(l => l.LineType == MonitorLineType.Bus || l.LineType == MonitorLineType.BusB).OrderBy(l => l.Reihenfolge);
            var nightbuses = modelLinien.Where(l => l.LineType == MonitorLineType.NightBus).OrderBy(l => l.Reihenfolge);

            mvm.SetLines(metros);
            tvm.SetLines(trams);
            bvm.SetLines(buses);
            nbvm.SetLines(nightbuses);
        }
    }
}
