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
            // No need to store navigation properties

            Property(vm => vm.Richtung)
                .InPhoneState()
                .RestoreAfterActivation();  // this property is needed in OnActivate
        }
    }
}
