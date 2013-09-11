using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Resources;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class NewRouteViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        public NewRouteViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            DisplayName = "new route";

            FromStationName = AppResources.Routing_SourceStationHintText;
            ToStationName = AppResources.Routing_DestinationStationHintText;

            DateOfTrip = DateTime.Now.Date;
            TimeOfTrip = DateTime.Now;
        
            RoutingOptions = new List<string>() { "fastest", "fewest changes"};
        }

        // TEMPORARY
        public List<string> RoutingOptions { get; set; }
        public string SelectedRoutingOption { get; set; }

        private int? _fromStationId;
        private int? _toStationId;
        private string _fromStationName;
        private string _toStationName;

        public string FromStationName
        {
            get { return _fromStationName; }
            set { _fromStationName = value; NotifyOfPropertyChange(() => FromStationName); }
        }

        public string ToStationName
        {
            get { return _toStationName; }
            set { _toStationName = value; NotifyOfPropertyChange(() => ToStationName); }
        }

        public DateTime DateOfTrip { get; set; }
        public DateTime TimeOfTrip { get; set; }

        public void SelectFrom()
        {
            PerformStationSelection((i, s) =>
            {
                _fromStationId = i;
                FromStationName = s;
            });
        }

        public void SelectTo()
        {
            PerformStationSelection((i, s) =>
            {
                _toStationId = i;
                ToStationName = s;
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

        public void ReverseFromTo()
        {
            int? tempStationId = _fromStationId;
            string tempStationName = FromStationName;

            _fromStationId = _toStationId;
            FromStationName = _fromStationId == null ? AppResources.Routing_SourceStationHintText : ToStationName;
            _toStationId = tempStationId;
            ToStationName = _toStationId == null ? AppResources.Routing_DestinationStationHintText : tempStationName;
        }

        public bool CanSearchTrips
        {
            get
            {
                return true;
            }
        }

        public void SearchTrips()
        {
            
        }
    }
}
