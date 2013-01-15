using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Model
{
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Action<INavigationService> Navigate { get; set; }
    }
}
