using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.Routing;

namespace MundlTransit.WP8.StorageHandlers
{
    public class TripsViewModelStorage : StorageHandler<TripsViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.From)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.To)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.CurrentRoutingRequest)
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
