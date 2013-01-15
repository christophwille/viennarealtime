using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundlTransit.WP8.Model;
using Windows.Devices.Geolocation;

namespace MundlTransit.WP8.Common
{
    //
    // http://stackoverflow.com/questions/3695224/android-sqlite-getting-nearest-locations-with-latitude-and-longitude/12997900#12997900
    //
    public static class NearestLocationHelpers
    {
        public static Wgs84Location CalculateDerivedPosition(Wgs84Location point, double range, double bearing)
        {
            const double EarthRadius = 6371000; // m

            double latA = DegreeToRadian(point.Latitude);
            double lonA = DegreeToRadian(point.Longitude);

            double angularDistance = range / EarthRadius;
            double trueCourse = DegreeToRadian(bearing);

            double lat = Math.Asin(
                    Math.Sin(latA) * Math.Cos(angularDistance) +
                            Math.Cos(latA) * Math.Sin(angularDistance)
                            * Math.Cos(trueCourse));

            double dlon = Math.Atan2(
                    Math.Sin(trueCourse) * Math.Sin(angularDistance)
                            * Math.Cos(latA),
                    Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

            double lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

            lat = RadianToDegree(lat);
            lon = RadianToDegree(lon);

            var newPoint = new Wgs84Location((float) lon, (float) lat);
            return newPoint;

        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static bool LocationIsInDistance(Wgs84Location pointForCheck, Wgs84Location center, double radius)
        {
            if (GetDistanceBetweenTwoPoints(pointForCheck, center) <= radius)
                return true;
            else
                return false;
        }

        public static double GetDistanceBetweenTwoPoints(Wgs84Location p1, double p2Lon, double p2Lat)
        {
            return GetDistanceBetweenTwoPoints(p1, new Wgs84Location(p2Lon, p2Lat));
        }

        public static double GetDistanceBetweenTwoPoints(Wgs84Location p1, Wgs84Location p2)
        {
            const double R = 6371000; // m

            double dLat = DegreeToRadian(p2.Latitude - p1.Latitude);
            double dLon = DegreeToRadian(p2.Longitude - p1.Longitude);
            double lat1 = DegreeToRadian(p1.Latitude);
            double lat2 = DegreeToRadian(p2.Latitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2)
                    * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }
    }
}
