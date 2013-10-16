using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;

namespace MundlTransit.WP8.Common
{
    public static class PanoramaExtensions
    {
        public static void SlideToPage(this Panorama self, int item)
        {
            self.SlideToPageViaSelectedItemNoAnimation(item);
        }

        public static void SlideToPageNoAnimation(this Panorama self, int item)
        {
            // this changes the order (more than 2 panorama items will show the effect)
            self.DefaultItem = self.Items[item];
        }

        //
        // http://social.msdn.microsoft.com/Forums/wpapps/en-us/27f96ac9-5939-4d64-9ee0-4746a64fe28a/how-to-programmatically-set-the-visible-item-in-a-panorama-control?forum=wpdevelop
        //
        public static void SlideToPageViaSelectedItemNoAnimation(this Panorama self, int item)
        {
            self.SetValue(Panorama.SelectedItemProperty, self.Items[item]);
            self.Measure(new Size());
        }

        //
        // http://stackoverflow.com/questions/5350242/programatically-slide-to-next-panorama-item
        // This one animates very ugly
        //
        public static void SlideToPageSlideTransition(this Panorama self, int item)
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

        // Other possible solutions, not tried:
        //
        // http://xme.im/slide-or-change-panorama-selected-item-programatically
        //
    }
}
