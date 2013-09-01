using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Resources;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class TramViewModel : LineTypeBaseViewModel
    {
        public TramViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            DisplayName = AppResources.Lines_Tram;
        }
    }
}
