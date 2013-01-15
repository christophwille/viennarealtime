using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace MundlTransit.WP8.Common
{
    //
    // http://stackoverflow.com/questions/5350242/programatically-slide-to-next-panorama-item
    //
    public static class PanoramaExtensions
    {
        public static void SlideToPage(this Panorama self, int item)
        {
            var slide_transition = new SlideTransition() { };
            slide_transition.Mode = SlideTransitionMode.SlideLeftFadeIn;
            ITransition transition = slide_transition.GetTransition(self);
            transition.Completed += delegate
            {
                self.DefaultItem = self.Items[item];
                transition.Stop();
            };

            transition.Begin();
        }
    }
}
