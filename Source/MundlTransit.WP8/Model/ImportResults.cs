using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundlTransit.WP8.Model
{
    public class ImportResults
    {
        public ImportResults()
        {
            Succeeded = false;
        }

        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public int HaltestellenCount { get; set; }
        public int LinienCount { get; set; }
        public int SteigeCount { get; set; }
        public int LookupCount { get; set; }
    }
}
