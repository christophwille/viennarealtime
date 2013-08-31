using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.Data.Reference;

namespace MundlTransit.WP8.Common
{
    public static class ScreenExtensions
    {
        public static void WhenSelectionChanged<T>(this Screen screen, object sender, Action<T> action)
            where T: class
        {
            var ll = sender as LongListSelector;
            if (null == ll) return;

            var item = ll.SelectedItem as T;
            if (item == null) return;

            ll.SelectedItem = null;

            action(item);
        }
    }
}
