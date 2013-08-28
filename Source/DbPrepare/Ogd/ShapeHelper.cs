using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbPrepare.Ogd
{
    public static class ShapeHelper
    {
        // POINT (16.389769898563777 48.173790150890646)
        public static bool TryParseShape(string shape, out double longitude, out double latitude)
        {
            try
            {
                string pointShape = shape;

                int openParensPos = pointShape.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);
                string point = pointShape.Substring(++openParensPos, pointShape.Length - openParensPos - 1);

                var lonlat = point.Split(new char[] { ' ' });

                bool lonOk = Double.TryParse(lonlat[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out longitude);
                bool latOk = Double.TryParse(lonlat[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out latitude);

                return (lonOk && latOk);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());

                longitude = 0.0f;
                latitude = 0.0f;
                return false;
            }
        }
    }
}
