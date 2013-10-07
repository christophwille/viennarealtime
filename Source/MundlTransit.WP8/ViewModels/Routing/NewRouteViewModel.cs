using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.Views.Routing;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class NewRouteViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public NewRouteViewModel(IWindowManager windowManager, INavigationService navigationService, IDataService dataService)
        {
            _windowManager = windowManager;
            _navigationService = navigationService;
            _dataService = dataService;
            DisplayName = AppResources.NewRouteView_DisplayName;

            FromStationName = AppResources.Routing_SourceStationHintText;
            ToStationName = AppResources.Routing_DestinationStationHintText;

            SetToNow();

            RoutingOptions = new BindableCollection<RouteTypeOptionModel>() { 
                new RouteTypeOptionModel()
                {
                    RouteType = RouteTypeOption.LeastTime,
                    DisplayName = AppResources.NewRouteView_RoutingType_Fastest
                },
                new RouteTypeOptionModel()
                {
                    RouteType = RouteTypeOption.LeastInterchange,
                    DisplayName = AppResources.NewRouteView_RoutingType_FewestChanges
                },
                new RouteTypeOptionModel()
                {
                    RouteType = RouteTypeOption.LeastWalking,
                    DisplayName = AppResources.NewRouteView_RoutingType_LeastWalking
                }
            };

            // SelectedRoutingOption = RoutingOptions.First();
        }

        public IObservableCollection<RouteTypeOptionModel> RoutingOptions { get; set; }

        private RouteTypeOptionModel _routeType;
        public RouteTypeOptionModel SelectedRoutingOption
        {
            get { return _routeType; }
            set { _routeType = value; NotifyOfPropertyChange(() => SelectedRoutingOption); }
        }

        private int? _fromStationId;
        private int? _toStationId;
        private string _fromStationName;
        private string _toStationName;
        private DateTime? _dateOfTrip;
        private DateTime? _timeOfTrip;

        public string FromStationName
        {
            get { return _fromStationName; }
            set { _fromStationName = value; NotifyOfPropertyChange(() => FromStationName); NotifyOfPropertyChange(() => CanSearchTrips); }
        }

        public string ToStationName
        {
            get { return _toStationName; }
            set { _toStationName = value; NotifyOfPropertyChange(() => ToStationName); NotifyOfPropertyChange(() => CanSearchTrips); }
        }

        public DateTime? DateOfTrip
        {
            get { return _dateOfTrip; }
            set { _dateOfTrip = value; NotifyOfPropertyChange(() => DateOfTrip); NotifyOfPropertyChange(() => CanSearchTrips); }
        }

        public DateTime? TimeOfTrip
        {
            get { return _timeOfTrip; }
            set { _timeOfTrip = value; NotifyOfPropertyChange(() => TimeOfTrip); NotifyOfPropertyChange(() => CanSearchTrips); }
        }

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

        private void PerformStationSelection(Action<int, string> onSuccess)
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
                return (_fromStationId != null && _toStationId != null && _dateOfTrip != null && _timeOfTrip != null);
            }
        }

        public static RoutingRequest CurrentRoutingRequest { get; set; }

        public async void SearchTrips()
        {
            var fromStation = await _dataService.GetHaltestelleAsync(_fromStationId.Value);
            var toStation = await _dataService.GetHaltestelleAsync(_toStationId.Value);

            CurrentRoutingRequest = new RoutingRequest()
            {
                FromStation = fromStation.Diva,
                ToStation = toStation.Diva,
                RouteType = SelectedRoutingOption == null ? RouteTypeOption.LeastTime : SelectedRoutingOption.RouteType,
                When = new DateTime(_dateOfTrip.Value.Year, _dateOfTrip.Value.Month, _dateOfTrip.Value.Day,
                    _timeOfTrip.Value.Hour, _timeOfTrip.Value.Minute, 0)
            };

            _navigationService.UriFor<TripsViewModel>()
                .WithParam(m => m.From, FromStationName)
                .WithParam(m => m.To, ToStationName)
                .Navigate();
        }

        public void SetToNow()
        {
            var now = DateTime.Now;

            DateOfTrip = now.Date;
            TimeOfTrip = now;
        }
    }
}
