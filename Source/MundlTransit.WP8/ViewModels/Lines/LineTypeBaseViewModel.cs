using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.Data.Reference;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class LineTypeBaseViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public LineTypeBaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void SetLines(IEnumerable<OgdLinie> lines)
        {
            Lines = new BindableCollection<OgdLinie>(lines);
            NotifyOfPropertyChange(() => Lines);
        }

        public BindableCollection<OgdLinie> Lines { get; set; }

        public void ShowLine(object sender)
        {
            var ll = sender as LongListSelector;
            var item = ll.SelectedItem as OgdLinie;
            if (item == null) return;

            ll.SelectedItem = null;

            //navigationService.UriFor<StationInfoPivotPageViewModel>()
            //    .WithParam(vm => vm.NavigationStationId, item.HaltestellenId)
            //    .Navigate();
        }
    }
}
