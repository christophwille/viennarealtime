using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MundlTransit.WP8.Common;
using WienerLinien.Api;

namespace MundlTransit.WP8.Views.StationInfo
{

 public class LineTypeSelector : DataTemplateSelector
    {
        public DataTemplate Metro {
            get;
            set;
        }

        public DataTemplate Default
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MonitorLine line = item as MonitorLine;

            if (line != null)
            {
                // TODO: disabled
                // This is the only test case at the moment
                //if (line.Type == MonitorLineType.Metro && line.Name == "U1")
                //{
                //    return Metro;
                //}
                //else
                {
                    return Default;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
