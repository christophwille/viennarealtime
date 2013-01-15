using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.Stations;

namespace MundlTransit.WP8.StorageHandlers
{
    public class StationsSearchViewModelStorage : StorageHandler<StationsSearchViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.SearchText)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.Haltestellen)
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
