using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class NewRouteViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        public NewRouteViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            DisplayName = "new";
        }

        public void SelectFrom()
        {
            var vm = IoC.Get<StationSelectorViewModel>();
            _windowManager.ShowDialog(vm);
        }
    }
}
