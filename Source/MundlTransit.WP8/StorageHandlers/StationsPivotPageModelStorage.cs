using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.Stations;

namespace MundlTransit.WP8.StorageHandlers
{
    public class StationsPivotPageModelStorage : StorageHandler<StationsPivotPageViewModel>
    {
        public override void Configure()
        {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
