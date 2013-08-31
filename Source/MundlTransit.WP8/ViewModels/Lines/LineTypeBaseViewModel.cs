using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Model;
using MundlTransit.WP8.ViewModels.LineInfo;

namespace MundlTransit.WP8.ViewModels.Lines
{
    public class LineTypeBaseViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public LineTypeBaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void SetLines(IEnumerable<LinieModel> lines)
        {
            Lines = new BindableCollection<LinieModel>(lines);
            NotifyOfPropertyChange(() => Lines);
        }

        public BindableCollection<LinieModel> Lines { get; set; }

        public void ShowLine(object sender)
        {
            this.WhenSelectionChanged<LinieModel>(sender, (item) =>
            {
                _navigationService.UriFor<LineInfoPageViewModel>()
                    .WithParam(vm => vm.NavigationLineId, item.Id)
                    .WithParam(vm => vm.LineName, item.Bezeichnung)
                    .Navigate();
            });
        }
    }
}
