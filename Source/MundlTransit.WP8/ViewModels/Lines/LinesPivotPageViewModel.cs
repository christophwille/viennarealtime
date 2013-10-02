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
using WienerLinien.Api.Realtime;

namespace MundlTransit.WP8.ViewModels.Lines
{
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
    }
}
