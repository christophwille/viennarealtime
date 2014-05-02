using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.ApplicationInsights.Telemetry.WindowsStore;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.Views.Routing;
using Newtonsoft.Json;
using WienerLinien.Api.Routing;

namespace MundlTransit.WP8.ViewModels.Routing
{
    public class NewRouteViewModel : Screen, IHandle<ShowNewRouteViewMessage>
    {
        private readonly IWindowManager _windowManager;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public NewRouteViewModel(IWindowManager windowManager, INavigationService navigationService, IDataService dataService, IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);

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
            NotifyOfPropertyChange(() => RoutingOptions);

            SelectedRoutingOptionIndex = 0;
        }

        public IObservableCollection<RouteTypeOptionModel> RoutingOptions { get; set; }

        private int _selectedRoutingOptionIndex;
        public int SelectedRoutingOptionIndex
        {
            get { return _selectedRoutingOptionIndex; }
            set { _selectedRoutingOptionIndex = value; NotifyOfPropertyChange(() => SelectedRoutingOptionIndex); }
        }

        public int? FromStationId { get; set; }
        public int? ToStationId { get; set; }

        // Backing fields for bindable properties
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
                FromStationId = i;
                FromStationName = s;
            });
        }

        public void SelectTo()
        {
            PerformStationSelection((i, s) =>
            {
                ToStationId = i;
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
            int? tempStationId = FromStationId;
            string tempStationName = FromStationName;

            FromStationId = ToStationId;
            FromStationName = FromStationId == null ? AppResources.Routing_SourceStationHintText : ToStationName;
            ToStationId = tempStationId;
            ToStationName = ToStationId == null ? AppResources.Routing_DestinationStationHintText : tempStationName;
        }

        public bool CanSearchTrips
        {
            get
            {
                return (FromStationId != null && ToStationId != null && _dateOfTrip != null && _timeOfTrip != null);
            }
        }

        public async void SearchTrips()
        {
            ClientAnalyticsChannel.Default.LogEvent("Routing/Search");

            // Get the Haltestelle (for names and more)
            var fromStation = await _dataService.GetHaltestelleAsync(FromStationId.Value);
            var toStation = await _dataService.GetHaltestelleAsync(ToStationId.Value);

            // Create a routing request and convert it to JSON
            RouteTypeOptionModel optionModel = RoutingOptions[SelectedRoutingOptionIndex];

            var currentRequest = new RoutingRequest()
            {
                FromStation = fromStation.Diva,
                ToStation = toStation.Diva,
                RouteType = optionModel.RouteType,
                When = new DateTime(_dateOfTrip.Value.Year, _dateOfTrip.Value.Month, _dateOfTrip.Value.Day,
                    _timeOfTrip.Value.Hour, _timeOfTrip.Value.Minute, 0)
            };

            string jsonRoutingRequest = JsonConvert.SerializeObject(currentRequest);

            // Store the history entry
            await _dataService.InsertRouteHistoryItemAsync(new RouteHistoryItem()
                    {
                        From = FromStationName,
                        To = ToStationName,
                        FromHaltestelleId = FromStationId.Value,
                        ToHaltestelleId = ToStationId.Value,
                        RouteType = (int)currentRequest.RouteType
                    });

            // Navigate
            _navigationService.UriFor<TripsViewModel>()
                .WithParam(m => m.From, FromStationName)
                .WithParam(m => m.To, ToStationName)
                .WithParam(m => m.CurrentRoutingRequest, jsonRoutingRequest)
                .Navigate();
        }

        public void SetToNow()
        {
            var now = DateTime.Now;

            DateOfTrip = now.Date;
            TimeOfTrip = now;
        }

        public void Handle(ShowNewRouteViewMessage message)
        {
            var rhi = message.RouteHistory;

            FromStationId = rhi.FromHaltestelleId;
            ToStationId = rhi.ToHaltestelleId;
            FromStationName = rhi.From;
            ToStationName = rhi.To;

            // Find the index in our RouteTypeOptionModel collection and set SelectedRoutingOptionIndex
            var routeOption = (RouteTypeOption)rhi.RouteType;
            int index = 0;
            foreach (var rom in RoutingOptions)
            {
                if (rom.RouteType == routeOption)
                {
                    SelectedRoutingOptionIndex = index;
                    break;
                }
                index++;
            }

            // Always use the current date / time
            SetToNow();
        }
    }
}
