using Caliburn.Micro;
using MundlTransit.WP8.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8
{
    public class SettingsPageViewModel : Screen
    {
        private readonly ILocationService _locationService;

        public SettingsPageViewModel(ILocationService locsvc)
        {
            _locationService = locsvc;
        }

        // OnInitialize is called only once, we want to do it on every activation of this screen
        protected override void OnActivate()
        {
            base.OnActivate();

            isChecked = _locationService.GetCurrentConsentValue();
            NotifyOfPropertyChange(() => Checked);
        }

        private bool? isChecked;
        public bool? Checked
        {
            get { return isChecked; }
            set
            {
                if (value.HasValue)
                {
                    _locationService.SetConsentValue(value.Value);
                    isChecked = value;

                    NotifyOfPropertyChange(() => Checked);
                }
            }
        }
    }
}
