using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Data.Reference
{
    // This is not a Table but a View
    public class LinienHaltestelleView : Haltestelle
    {
        public string Richtung { get; set; }
        public int Reihenfolge { get; set; }
    }
}
