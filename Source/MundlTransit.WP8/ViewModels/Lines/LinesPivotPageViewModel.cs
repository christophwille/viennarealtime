using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Lines
{
    // "list" "search" "nearby" "line" (use expanderview)
    public class LinesPivotPageViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private MetroViewModel mvm; 
        private TramViewModel tvm; 
        private BusViewModel bvm; 
        private NightBusViewModel nbvm;

        public LinesPivotPageViewModel(MetroViewModel mvm, TramViewModel tvm, BusViewModel bvm, NightBusViewModel nbvm)
        {
            this.mvm = mvm;
            this.tvm = tvm;
            this.bvm = bvm;
            this.nbvm = nbvm;
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
