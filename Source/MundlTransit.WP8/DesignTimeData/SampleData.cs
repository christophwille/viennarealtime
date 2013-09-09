using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Model;
using WienerLinien.Api;

namespace MundlTransit.WP8.DesignTimeData
{
    public class SampleData
    {
        public ObservableCollection<SampleLineData> Lines { get; set; }

        public ObservableCollection<SampleHaltestelleData> Stations { get; set; }
        public ObservableCollection<SampleHaltestelleData> Haltestellen { get; set; }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public ObservableCollection<TrafficInformationItem> TrafficInformation { get; set; }

        public ObservableCollection<MonitorLine> Departures { get; set; }

        public SampleData()
        {
            Lines = new ObservableCollection<SampleLineData>(CreateLineSampleData());
            Stations = Haltestellen = new ObservableCollection<SampleHaltestelleData>(CreateStationsSampleData());
            MenuItems = new ObservableCollection<MenuItem>(CreateSampleMenuItems());
            TrafficInformation = new ObservableCollection<TrafficInformationItem>(CreateTrafficInformationItems());
            Departures = new ObservableCollection<MonitorLine>(CreateDepartures());
        }

        private List<SampleLineData> CreateLineSampleData()
        {
            var lines = new List<SampleLineData>()
            {
                new SampleLineData()
                {
                    Bezeichnung = "U1"
                },
                new SampleLineData()
                {
                    Bezeichnung = "U2"
                },
                new SampleLineData()
                {
                    Bezeichnung = "U5"
                }
            };

            return lines;
        }

        private List<SampleHaltestelleData> CreateStationsSampleData()
        {
            var stations = new List<SampleHaltestelleData>()
            {
                new SampleHaltestelleData()
                {
                    Bezeichnung = "Siebenhirten",
                    SecondaryInformation = "U6",
                    DisplayDistanz = "150m",
                },
                new SampleHaltestelleData()
                {
                    Bezeichnung = "Schottenring",
                    SecondaryInformation = "U2, U4, 1, 31, 1A",
                    DisplayDistanz = "193m",
                },
                new SampleHaltestelleData()
                {
                    Bezeichnung = "Heiligenstadt",
                    SecondaryInformation = "U4, D, and, some, more, definitely",
                    DisplayDistanz = "401m",
                }
            };

            return stations;
        }

        private List<MenuItem> CreateSampleMenuItems()
        {
            var menuitems = new List<MenuItem>()
            {
                new MenuItem
                {
                    Name = "nearby",
                    Description = "stations nearby your current location",
                },
                new MenuItem
                {
                    Name = "search",
                    Description = "stations by name (fulltext)",
                },
                new MenuItem
                {
                    Name = "station list",
                    Description = "complete list of stations",
               }
            };

            return menuitems;
        }

        private List<TrafficInformationItem> CreateTrafficInformationItems()
        {
            var items = new List<TrafficInformationItem>()
            {
                new TrafficInformationItem()
                {
                    Title = "49 Verspätung",
                    Description = "Verkehrsbedingt kommt es derzeit bei der Linie 49 in beiden Fahrtrichtungen zu längeren Wartezeiten.",
                    RelatedLines = "49"
                },
                new TrafficInformationItem()
                {
                    Title = "66A Verkehrsunfall",
                    Description = "Nach einer Fahrtbehinderung kommt es derzeit zu längeren Wartezeiten.",
                    RelatedLines = "66A"
                }
            };

            return items;
        }

        private List<MonitorLine> CreateDepartures()
        {
            var deps = new List<MonitorLine>()
            {
                new MonitorLine()
                {
                    Name = "U4",
                    Towards = "Heiligenstadt",
                    Departures = new List<Departure>() {
                                    new Departure()
                                    {
                                        Name = "U4",
                                        Towards = "Heiligenstadt",
                                        RealtimeSupported = true,
                                        BarrierFree = true,
                                        Countdown = 3,
                                        Type = MonitorLineType.Metro
                                    },
                                    new Departure()
                                    {
                                        Name = "U4",
                                        Towards = "Heiligenstadt",
                                        RealtimeSupported = true,
                                        BarrierFree = false,
                                        Countdown = 7,
                                        Type = MonitorLineType.Metro
                                    },
                                    new Departure()
                                    {
                                        Name = "U4",
                                        Towards = "Heiligenstadt",
                                        RealtimeSupported = true,
                                        BarrierFree = true,
                                        Countdown = 14,
                                        Type = MonitorLineType.Metro
                                    },
                                    new Departure()
                                    {
                                        Name = "U4",
                                        Towards = "Heiligenstadt",
                                        RealtimeSupported = true,
                                        BarrierFree = true,
                                        Countdown = 45,
                                        Type = MonitorLineType.Metro
                                    }
                    }
                }
            };

            return deps;
        }
    }
}
