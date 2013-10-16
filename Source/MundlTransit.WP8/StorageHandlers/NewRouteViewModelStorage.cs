using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.Routing;

namespace MundlTransit.WP8.StorageHandlers
{
    public class NewRouteViewModelStorage : StorageHandler<NewRouteViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.FromStationId)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.ToStationId)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.FromStationName)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.ToStationName)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.SelectedRoutingOptionIndex)
                .InPhoneState()
                .RestoreAfterViewLoad();

            // Date / Time of trip is intentionally not stored (SetToNow *is* a good idea)
        }
    }
}
