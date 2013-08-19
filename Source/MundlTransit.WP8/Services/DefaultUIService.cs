using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Coding4Fun.Toolkit.Controls;

namespace MundlTransit.WP8.Services
{
    public class DefaultUIService : IUIService
    {
        // http://coding4fun.codeplex.com/wikipage?title=Toast%20Prompt&referringTitle=Documentation
        public void ShowTextToast(string title, string message, int milliseconds = 1000)
        {
            var toast = new ToastPrompt()
            {
                Title = title,
                Message = message,
                FontSize = 30,
                TextOrientation = System.Windows.Controls.Orientation.Vertical,
                MillisecondsUntilHidden = milliseconds
            };

            toast.Show();
        }
    }
}
