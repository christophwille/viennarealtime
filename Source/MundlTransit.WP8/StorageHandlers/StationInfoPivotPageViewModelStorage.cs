using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.StationInfo;

namespace MundlTransit.WP8.StorageHandlers
{
    public class StationInfoPivotPageViewModelStorage : StorageHandler<StationInfoPivotPageViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.NavigationStationId)
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
