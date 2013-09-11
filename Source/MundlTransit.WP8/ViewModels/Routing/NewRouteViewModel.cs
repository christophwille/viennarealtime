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

            FromStationName = "Select start";
            ToStationName = "Select destination";
        }

        private int? _fromStationId;
        private int? _toStationId;

        public string FromStationName { get; set; }
        public string ToStationName { get; set; }

        public void SelectFrom()
        {
            PerformStationSelection((i, s) =>
            {
                _fromStationId = i;
                FromStationName = s;
                NotifyOfPropertyChange(() => FromStationName);
            });
        }

        public void SelectTo()
        {
            PerformStationSelection((i, s) =>
            {
                _toStationId = i;
                ToStationName = s;
                NotifyOfPropertyChange(() => ToStationName);
            });
        }

        private void PerformStationSelection(Action<int,string> onSuccess)
        {
            var vm = IoC.Get<StationSelectorViewModel>();
            _windowManager.ShowDialog(vm);

            vm.Deactivated += (sender, args) =>
            {
                if (args.WasClosed)
                {
                    if (null != vm.PickedStation)
                        onSuccess(vm.PickedStation.Value, vm.PickedStationName);
                }
            };
        }
    }
}
