using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.ViewModels
{
    public interface IStationPicker
    {
        Action<int, string> OnStationPicked { get; set; }
    }
}
