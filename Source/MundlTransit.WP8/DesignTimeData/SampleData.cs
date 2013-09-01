using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Model;

namespace MundlTransit.WP8.DesignTimeData
{
    public class SampleData
    {
        public ObservableCollection<SampleLineData> Lines { get; set; }

        public ObservableCollection<SampleHaltestelleData> Stations { get; set; }
        public ObservableCollection<SampleHaltestelleData> Haltestellen { get; set; }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public SampleData()
        {
            Lines = new ObservableCollection<SampleLineData>(CreateLineSampleData());
            Stations = Haltestellen = new ObservableCollection<SampleHaltestelleData>(CreateStationsSampleData());
            MenuItems = new ObservableCollection<MenuItem>(CreateSampleMenuItems());
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
    }
}
