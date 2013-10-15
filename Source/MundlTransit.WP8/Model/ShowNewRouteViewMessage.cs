using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Runtime;

namespace MundlTransit.WP8.Model
{
    public class ShowNewRouteViewMessage
    {
        public ShowNewRouteViewMessage(RouteHistoryItem item)
        {
            RouteHistory = item;
        }

        public RouteHistoryItem RouteHistory { get; private set; }
    }
}
