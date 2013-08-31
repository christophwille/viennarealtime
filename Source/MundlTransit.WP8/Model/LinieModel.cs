using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Data.Reference;
using WienerLinien.Api;
using WienerLinien.Api.Ogd;

namespace MundlTransit.WP8.Model
{
    public class LinieModel
    {
        public MonitorLineType LineType { get; set; }

        private readonly OgdLinie _ogdLinie;

        public LinieModel(OgdLinie ol)
        {
            _ogdLinie = ol;
            LineType = MonitorLineTypeMapper.TypeStringToType(ol.Verkehrsmittel);
        }

        public int Id { get { return _ogdLinie.Id; } }
        public string Bezeichnung { get { return _ogdLinie.Bezeichnung; } }
        public int Reihenfolge { get { return _ogdLinie.Reihenfolge; } }
        public bool Echtzeit { get { return _ogdLinie.Echtzeit; } }
    }
}
