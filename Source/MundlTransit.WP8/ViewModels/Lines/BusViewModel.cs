using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class BusViewModel : LineTypeBaseViewModel
    {
        public BusViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            DisplayName = "bus";
        }
    }
}
