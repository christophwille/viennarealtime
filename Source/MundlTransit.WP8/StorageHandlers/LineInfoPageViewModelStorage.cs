using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.ViewModels.LineInfo;

namespace MundlTransit.WP8.StorageHandlers
{
    public class LineInfoPageViewModelStorage : StorageHandler<LineInfoPageViewModel>
    {
        public override void Configure()
        {
            Property(vm => vm.NavigationLineId)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.NavigationLineName)
                .InPhoneState()
                .RestoreAfterViewLoad();

            Property(vm => vm.Richtung)
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}
