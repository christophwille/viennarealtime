using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class TramViewModel : LineTypeBaseViewModel
    {
        public TramViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            DisplayName = "tram";
        }
    }
}
