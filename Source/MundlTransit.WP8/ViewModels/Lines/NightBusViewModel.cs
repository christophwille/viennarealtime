using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using WienerLinien.Api;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class NightBusViewModel : LineTypeBaseViewModel
    {
        public NightBusViewModel(INavigationService navigationService, IDataService dataSvcService)
            : base(navigationService, dataSvcService)
        {
            DisplayName = AppResources.Lines_NightBus;
            SetSingleLineType(MonitorLineType.NightBus);
        }
    }
}
